
<?php
class  M_bp3doc_uk extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3doc_ukid) as bp3doc_ukid, B2G(bp3doc_ukid) as id,B2G(instanceid) as instanceid, bp3doc_uk_BRIEF_F(bp3doc_ukid , NULL) as  brief,perparent, case perparent  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as perparent_grid,name,thecomment,B2G(UKStore) ukstore, bp3doc_store_BRIEF_F(ukstore, NULL) as ukstore_grid', 'PartName' => 'bp3doc_uk', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3doc_ukid'])) {
	        $data['bp3doc_ukid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3doc_uk', 'RowID' => $data['bp3doc_ukid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3doc_uk', 'RowID' => $data['bp3doc_ukid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3doc_ukid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3doc_uk', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_ukid) as bp3doc_ukid, B2G(bp3doc_ukid) as id,B2G(instanceid) as instanceid, bp3doc_uk_BRIEF_F(bp3doc_ukid , NULL) as  brief,perparent, case perparent  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as perparent_grid,name,thecomment,B2G(UKStore) ukstore, bp3doc_store_BRIEF_F(ukstore, NULL) as ukstore_grid', 'ViewName' => 'bp3doc_uk'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_ukid) as bp3doc_ukid, B2G(bp3doc_ukid) as id,B2G(instanceid) as instanceid, bp3doc_uk_BRIEF_F(bp3doc_ukid , NULL) as  brief,perparent, case perparent  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as perparent_grid,name,thecomment,B2G(UKStore) ukstore, bp3doc_store_BRIEF_F(ukstore, NULL) as ukstore_grid', 'ViewName' => 'bp3doc_uk', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_ukid) as bp3doc_ukid, B2G(bp3doc_ukid) as id,B2G(instanceid) as instanceid, bp3doc_uk_BRIEF_F(bp3doc_ukid , NULL) as  brief,perparent, case perparent  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as perparent_grid,name,thecomment,B2G(UKStore) ukstore, bp3doc_store_BRIEF_F(ukstore, NULL) as ukstore_grid', 'ViewName' => 'bp3doc_uk', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3doc_uk', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>