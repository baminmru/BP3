
<?php
class  M_excar_odo extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(excar_odoid) as excar_odoid, B2G(excar_odoid) as id,B2G(instanceid) as instanceid, exCar_odo_BRIEF_F(excar_odoid , NULL) as  brief,  DATE_FORMAT(thedate,\'%Y-%m-%d %H:%i:%s\') as  thedate,thevalue', 'PartName' => 'excar_odo', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['excar_odoid'])) {
	        $data['excar_odoid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'excar_odo', 'RowID' => $data['excar_odoid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'excar_odo', 'RowID' => $data['excar_odoid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['excar_odoid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'excar_odo', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(excar_odoid) as excar_odoid, B2G(excar_odoid) as id,B2G(instanceid) as instanceid, exCar_odo_BRIEF_F(excar_odoid , NULL) as  brief,  DATE_FORMAT(thedate,\'%Y-%m-%d %H:%i:%s\') as  thedate,thevalue', 'ViewName' => 'excar_odo'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(excar_odoid) as excar_odoid, B2G(excar_odoid) as id,B2G(instanceid) as instanceid, exCar_odo_BRIEF_F(excar_odoid , NULL) as  brief,  DATE_FORMAT(thedate,\'%Y-%m-%d %H:%i:%s\') as  thedate,thevalue', 'ViewName' => 'excar_odo', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(excar_odoid) as excar_odoid, B2G(excar_odoid) as id,B2G(instanceid) as instanceid, exCar_odo_BRIEF_F(excar_odoid , NULL) as  brief,  DATE_FORMAT(thedate,\'%Y-%m-%d %H:%i:%s\') as  thedate,thevalue', 'ViewName' => 'excar_odo', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'excar_odo', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>