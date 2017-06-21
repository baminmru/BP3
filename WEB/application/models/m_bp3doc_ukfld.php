
<?php
class  M_bp3doc_ukfld extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3doc_ukfldid) as bp3doc_ukfldid, B2G(bp3doc_ukfldid) as id,B2G(parentstructrowid) as parentid, bp3doc_ukfld_BRIEF_F(bp3doc_ukfldid , NULL) as  brief,B2G(TheField) thefield, FIELD_BRIEF_F(thefield, NULL) as thefield_grid', 'PartName' => 'bp3doc_ukfld', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3doc_ukfldid'])) {
	        $data['bp3doc_ukfldid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3doc_ukfld', 'RowID' => $data['bp3doc_ukfldid'], 'DocumentID' => $data['instanceid'],'ParentID' => $data['parentid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3doc_ukfld', 'RowID' => $data['bp3doc_ukfldid'], 'DocumentID' => $data['instanceid'],'ParentID' => $data['parentid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3doc_ukfldid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$parentid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3doc_ukfld', 'RowID' => $id, 'DocumentID' => $instanceid,'ParentID'=>$parentid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_ukfldid) as bp3doc_ukfldid, B2G(bp3doc_ukfldid) as id,B2G(parentstructrowid) as parentid, bp3doc_ukfld_BRIEF_F(bp3doc_ukfldid , NULL) as  brief,B2G(TheField) thefield, FIELD_BRIEF_F(thefield, NULL) as thefield_grid', 'ViewName' => 'bp3doc_ukfld'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_ukfldid) as bp3doc_ukfldid, B2G(bp3doc_ukfldid) as id,B2G(parentstructrowid) as parentid, bp3doc_ukfld_BRIEF_F(bp3doc_ukfldid , NULL) as  brief,B2G(TheField) thefield, FIELD_BRIEF_F(thefield, NULL) as thefield_grid', 'ViewName' => 'bp3doc_ukfld', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_ukfldid) as bp3doc_ukfldid, B2G(bp3doc_ukfldid) as id,B2G(parentstructrowid) as parentid, bp3doc_ukfld_BRIEF_F(bp3doc_ukfldid , NULL) as  brief,B2G(TheField) thefield, FIELD_BRIEF_F(thefield, NULL) as thefield_grid', 'ViewName' => 'bp3doc_ukfld', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3doc_ukfld', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>