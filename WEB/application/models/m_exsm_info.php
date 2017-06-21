
<?php
class  M_exsm_info extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(exsm_infoid) as exsm_infoid, B2G(exsm_infoid) as id,B2G(instanceid) as instanceid, exSm_info_BRIEF_F(exsm_infoid , NULL) as  brief,sclosed, case sclosed  when -1 then \'Да\' when 0 then \'Нет\'End  as sclosed_grid,B2G(theDriver) thedriver, exDrv_info_BRIEF_F(thedriver, NULL) as thedriver_grid,B2G(theCar) thecar, exCar_info_BRIEF_F(thecar, NULL) as thecar_grid,  DATE_FORMAT(thedate,\'%Y-%m-%d\') as  thedate,smnumber,start_balabns,end_balans', 'PartName' => 'exsm_info', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['exsm_infoid'])) {
	        $data['exsm_infoid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'exsm_info', 'RowID' => $data['exsm_infoid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'exsm_info', 'RowID' => $data['exsm_infoid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['exsm_infoid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'exsm_info', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(exsm_infoid) as exsm_infoid, B2G(exsm_infoid) as id,B2G(instanceid) as instanceid, exSm_info_BRIEF_F(exsm_infoid , NULL) as  brief,sclosed, case sclosed  when -1 then \'Да\' when 0 then \'Нет\'End  as sclosed_grid,B2G(theDriver) thedriver, exDrv_info_BRIEF_F(thedriver, NULL) as thedriver_grid,B2G(theCar) thecar, exCar_info_BRIEF_F(thecar, NULL) as thecar_grid,  DATE_FORMAT(thedate,\'%Y-%m-%d\') as  thedate,smnumber,start_balabns,end_balans', 'ViewName' => 'exsm_info'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(exsm_infoid) as exsm_infoid, B2G(exsm_infoid) as id,B2G(instanceid) as instanceid, exSm_info_BRIEF_F(exsm_infoid , NULL) as  brief,sclosed, case sclosed  when -1 then \'Да\' when 0 then \'Нет\'End  as sclosed_grid,B2G(theDriver) thedriver, exDrv_info_BRIEF_F(thedriver, NULL) as thedriver_grid,B2G(theCar) thecar, exCar_info_BRIEF_F(thecar, NULL) as thecar_grid,  DATE_FORMAT(thedate,\'%Y-%m-%d\') as  thedate,smnumber,start_balabns,end_balans', 'ViewName' => 'exsm_info', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(exsm_infoid) as exsm_infoid, B2G(exsm_infoid) as id,B2G(instanceid) as instanceid, exSm_info_BRIEF_F(exsm_infoid , NULL) as  brief,sclosed, case sclosed  when -1 then \'Да\' when 0 then \'Нет\'End  as sclosed_grid,B2G(theDriver) thedriver, exDrv_info_BRIEF_F(thedriver, NULL) as thedriver_grid,B2G(theCar) thecar, exCar_info_BRIEF_F(thecar, NULL) as thecar_grid,  DATE_FORMAT(thedate,\'%Y-%m-%d\') as  thedate,smnumber,start_balabns,end_balans', 'ViewName' => 'exsm_info', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'exsm_info', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>