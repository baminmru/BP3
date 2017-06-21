
<?php
class  M_bp3qry_link extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3qry_linkid) as bp3qry_linkid, B2G(bp3qry_linkid) as id,B2G(instanceid) as instanceid, bp3qry_link_BRIEF_F(bp3qry_linkid , NULL) as  brief,handjoin,B2G(TheJoinSource) thejoinsource, bp3qry_column_BRIEF_F(thejoinsource, NULL) as thejoinsource_grid,B2G(TheJoinDestination) thejoindestination, bp3qry_column_BRIEF_F(thejoindestination, NULL) as thejoindestination_grid,seq,reftype, case reftype  when 0 then \'Нет\' when 1 then \'Ссылка на объект\' when 2 then \'Ссылка на строку\' when 3 then \'Связка InstanceID (в передлах объекта)\' when 4 then \'Связка ParentStructRowID  (в передлах объекта)\' else \'\'  end   as reftype_grid,B2G(TheView) theview, bp3qry_def_BRIEF_F(theview, NULL) as theview_grid', 'PartName' => 'bp3qry_link', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3qry_linkid'])) {
	        $data['bp3qry_linkid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3qry_link', 'RowID' => $data['bp3qry_linkid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3qry_link', 'RowID' => $data['bp3qry_linkid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3qry_linkid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3qry_link', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3qry_linkid) as bp3qry_linkid, B2G(bp3qry_linkid) as id,B2G(instanceid) as instanceid, bp3qry_link_BRIEF_F(bp3qry_linkid , NULL) as  brief,handjoin,B2G(TheJoinSource) thejoinsource, bp3qry_column_BRIEF_F(thejoinsource, NULL) as thejoinsource_grid,B2G(TheJoinDestination) thejoindestination, bp3qry_column_BRIEF_F(thejoindestination, NULL) as thejoindestination_grid,seq,reftype, case reftype  when 0 then \'Нет\' when 1 then \'Ссылка на объект\' when 2 then \'Ссылка на строку\' when 3 then \'Связка InstanceID (в передлах объекта)\' when 4 then \'Связка ParentStructRowID  (в передлах объекта)\' else \'\'  end   as reftype_grid,B2G(TheView) theview, bp3qry_def_BRIEF_F(theview, NULL) as theview_grid', 'ViewName' => 'bp3qry_link'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3qry_linkid) as bp3qry_linkid, B2G(bp3qry_linkid) as id,B2G(instanceid) as instanceid, bp3qry_link_BRIEF_F(bp3qry_linkid , NULL) as  brief,handjoin,B2G(TheJoinSource) thejoinsource, bp3qry_column_BRIEF_F(thejoinsource, NULL) as thejoinsource_grid,B2G(TheJoinDestination) thejoindestination, bp3qry_column_BRIEF_F(thejoindestination, NULL) as thejoindestination_grid,seq,reftype, case reftype  when 0 then \'Нет\' when 1 then \'Ссылка на объект\' when 2 then \'Ссылка на строку\' when 3 then \'Связка InstanceID (в передлах объекта)\' when 4 then \'Связка ParentStructRowID  (в передлах объекта)\' else \'\'  end   as reftype_grid,B2G(TheView) theview, bp3qry_def_BRIEF_F(theview, NULL) as theview_grid', 'ViewName' => 'bp3qry_link', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3qry_linkid) as bp3qry_linkid, B2G(bp3qry_linkid) as id,B2G(instanceid) as instanceid, bp3qry_link_BRIEF_F(bp3qry_linkid , NULL) as  brief,handjoin,B2G(TheJoinSource) thejoinsource, bp3qry_column_BRIEF_F(thejoinsource, NULL) as thejoinsource_grid,B2G(TheJoinDestination) thejoindestination, bp3qry_column_BRIEF_F(thejoindestination, NULL) as thejoindestination_grid,seq,reftype, case reftype  when 0 then \'Нет\' when 1 then \'Ссылка на объект\' when 2 then \'Ссылка на строку\' when 3 then \'Связка InstanceID (в передлах объекта)\' when 4 then \'Связка ParentStructRowID  (в передлах объекта)\' else \'\'  end   as reftype_grid,B2G(TheView) theview, bp3qry_def_BRIEF_F(theview, NULL) as theview_grid', 'ViewName' => 'bp3qry_link', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3qry_link', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>