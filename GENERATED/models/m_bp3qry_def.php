
<?php
class  M_bp3qry_def extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3qry_defid) as bp3qry_defid, B2G(bp3qry_defid) as id,B2G(instanceid) as instanceid, bp3qry_def_BRIEF_F(bp3qry_defid , NULL) as  brief,name,the_alias,B2G(SrcPart) srcpart, bp3doc_store_BRIEF_F(srcpart, NULL) as srcpart_grid,forchoose, case forchoose  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as forchoose_grid', 'PartName' => 'bp3qry_def', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3qry_defid'])) {
	        $data['bp3qry_defid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3qry_def', 'RowID' => $data['bp3qry_defid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3qry_def', 'RowID' => $data['bp3qry_defid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3qry_defid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3qry_def', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3qry_defid) as bp3qry_defid, B2G(bp3qry_defid) as id,B2G(instanceid) as instanceid, bp3qry_def_BRIEF_F(bp3qry_defid , NULL) as  brief,name,the_alias,B2G(SrcPart) srcpart, bp3doc_store_BRIEF_F(srcpart, NULL) as srcpart_grid,forchoose, case forchoose  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as forchoose_grid', 'ViewName' => 'bp3qry_def'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3qry_defid) as bp3qry_defid, B2G(bp3qry_defid) as id,B2G(instanceid) as instanceid, bp3qry_def_BRIEF_F(bp3qry_defid , NULL) as  brief,name,the_alias,B2G(SrcPart) srcpart, bp3doc_store_BRIEF_F(srcpart, NULL) as srcpart_grid,forchoose, case forchoose  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as forchoose_grid', 'ViewName' => 'bp3qry_def', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3qry_defid) as bp3qry_defid, B2G(bp3qry_defid) as id,B2G(instanceid) as instanceid, bp3qry_def_BRIEF_F(bp3qry_defid , NULL) as  brief,name,the_alias,B2G(SrcPart) srcpart, bp3doc_store_BRIEF_F(srcpart, NULL) as srcpart_grid,forchoose, case forchoose  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as forchoose_grid', 'ViewName' => 'bp3qry_def', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3qry_def', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>