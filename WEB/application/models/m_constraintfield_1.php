
<?php
class  M_constraintfield_1 extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(constraintfield_1id) as constraintfield_1id, B2G(constraintfield_1id) as id,B2G(parentstructrowid) as parentid, CONSTRAINTFIELD_1_BRIEF_F(constraintfield_1id , NULL) as  brief,B2G(TheField) thefield, FIELD_BRIEF_F(thefield, NULL) as thefield_grid', 'PartName' => 'constraintfield_1', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['constraintfield_1id'])) {
	        $data['constraintfield_1id'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'constraintfield_1', 'RowID' => $data['constraintfield_1id'], 'DocumentID' => $data['instanceid'],'ParentID' => $data['parentid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'constraintfield_1', 'RowID' => $data['constraintfield_1id'], 'DocumentID' => $data['instanceid'],'ParentID' => $data['parentid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['constraintfield_1id'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$parentid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'constraintfield_1', 'RowID' => $id, 'DocumentID' => $instanceid,'ParentID'=>$parentid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(constraintfield_1id) as constraintfield_1id, B2G(constraintfield_1id) as id,B2G(parentstructrowid) as parentid, CONSTRAINTFIELD_1_BRIEF_F(constraintfield_1id , NULL) as  brief,B2G(TheField) thefield, FIELD_BRIEF_F(thefield, NULL) as thefield_grid', 'ViewName' => 'constraintfield_1'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(constraintfield_1id) as constraintfield_1id, B2G(constraintfield_1id) as id,B2G(parentstructrowid) as parentid, CONSTRAINTFIELD_1_BRIEF_F(constraintfield_1id , NULL) as  brief,B2G(TheField) thefield, FIELD_BRIEF_F(thefield, NULL) as thefield_grid', 'ViewName' => 'constraintfield_1', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(constraintfield_1id) as constraintfield_1id, B2G(constraintfield_1id) as id,B2G(parentstructrowid) as parentid, CONSTRAINTFIELD_1_BRIEF_F(constraintfield_1id , NULL) as  brief,B2G(TheField) thefield, FIELD_BRIEF_F(thefield, NULL) as thefield_grid', 'ViewName' => 'constraintfield_1', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'constraintfield_1', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>