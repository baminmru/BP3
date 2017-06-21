
<?php
class  M_excar_info extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(excar_infoid) as excar_infoid, B2G(excar_infoid) as id,B2G(instanceid) as instanceid, exCar_info_BRIEF_F(excar_infoid , NULL) as  brief,B2G(carMarkDic) carmarkdic, exDic_carMark_BRIEF_F(carmarkdic, NULL) as carmarkdic_grid,carnum,theyear,thecolor,vin,talon,passport,  DATE_FORMAT(osago,\'%Y-%m-%d\') as  osago,  DATE_FORMAT(kasko,\'%Y-%m-%d\') as  kasko', 'PartName' => 'excar_info', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['excar_infoid'])) {
	        $data['excar_infoid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'excar_info', 'RowID' => $data['excar_infoid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'excar_info', 'RowID' => $data['excar_infoid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['excar_infoid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'excar_info', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(excar_infoid) as excar_infoid, B2G(excar_infoid) as id,B2G(instanceid) as instanceid, exCar_info_BRIEF_F(excar_infoid , NULL) as  brief,B2G(carMarkDic) carmarkdic, exDic_carMark_BRIEF_F(carmarkdic, NULL) as carmarkdic_grid,carnum,theyear,thecolor,vin,talon,passport,  DATE_FORMAT(osago,\'%Y-%m-%d\') as  osago,  DATE_FORMAT(kasko,\'%Y-%m-%d\') as  kasko', 'ViewName' => 'excar_info'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(excar_infoid) as excar_infoid, B2G(excar_infoid) as id,B2G(instanceid) as instanceid, exCar_info_BRIEF_F(excar_infoid , NULL) as  brief,B2G(carMarkDic) carmarkdic, exDic_carMark_BRIEF_F(carmarkdic, NULL) as carmarkdic_grid,carnum,theyear,thecolor,vin,talon,passport,  DATE_FORMAT(osago,\'%Y-%m-%d\') as  osago,  DATE_FORMAT(kasko,\'%Y-%m-%d\') as  kasko', 'ViewName' => 'excar_info', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(excar_infoid) as excar_infoid, B2G(excar_infoid) as id,B2G(instanceid) as instanceid, exCar_info_BRIEF_F(excar_infoid , NULL) as  brief,B2G(carMarkDic) carmarkdic, exDic_carMark_BRIEF_F(carmarkdic, NULL) as carmarkdic_grid,carnum,theyear,thecolor,vin,talon,passport,  DATE_FORMAT(osago,\'%Y-%m-%d\') as  osago,  DATE_FORMAT(kasko,\'%Y-%m-%d\') as  kasko', 'ViewName' => 'excar_info', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'excar_info', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>