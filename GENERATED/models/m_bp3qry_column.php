
<?php
class  M_bp3qry_column extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3qry_columnid) as bp3qry_columnid, B2G(bp3qry_columnid) as id,B2G(instanceid) as instanceid, bp3qry_column_BRIEF_F(bp3qry_columnid , NULL) as  brief,name,B2G(FromPart) frompart, bp3doc_store_BRIEF_F(frompart, NULL) as frompart_grid,aggregation, case aggregation  when 0 then \'none\' when 1 then \'AVG\' when 2 then \'COUNT\' when 3 then \'SUM\' when 4 then \'MIN\' when 5 then \'MAX\' when 6 then \'CUSTOM\' else \'\'  end   as aggregation_grid,sequence,forcombo, case forcombo  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as forcombo_grid,expression,the_alias,B2G(Field) field, bp3doc_field_BRIEF_F(field, NULL) as field_grid', 'PartName' => 'bp3qry_column', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3qry_columnid'])) {
	        $data['bp3qry_columnid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3qry_column', 'RowID' => $data['bp3qry_columnid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3qry_column', 'RowID' => $data['bp3qry_columnid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3qry_columnid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3qry_column', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3qry_columnid) as bp3qry_columnid, B2G(bp3qry_columnid) as id,B2G(instanceid) as instanceid, bp3qry_column_BRIEF_F(bp3qry_columnid , NULL) as  brief,name,B2G(FromPart) frompart, bp3doc_store_BRIEF_F(frompart, NULL) as frompart_grid,aggregation, case aggregation  when 0 then \'none\' when 1 then \'AVG\' when 2 then \'COUNT\' when 3 then \'SUM\' when 4 then \'MIN\' when 5 then \'MAX\' when 6 then \'CUSTOM\' else \'\'  end   as aggregation_grid,sequence,forcombo, case forcombo  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as forcombo_grid,expression,the_alias,B2G(Field) field, bp3doc_field_BRIEF_F(field, NULL) as field_grid', 'ViewName' => 'bp3qry_column'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3qry_columnid) as bp3qry_columnid, B2G(bp3qry_columnid) as id,B2G(instanceid) as instanceid, bp3qry_column_BRIEF_F(bp3qry_columnid , NULL) as  brief,name,B2G(FromPart) frompart, bp3doc_store_BRIEF_F(frompart, NULL) as frompart_grid,aggregation, case aggregation  when 0 then \'none\' when 1 then \'AVG\' when 2 then \'COUNT\' when 3 then \'SUM\' when 4 then \'MIN\' when 5 then \'MAX\' when 6 then \'CUSTOM\' else \'\'  end   as aggregation_grid,sequence,forcombo, case forcombo  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as forcombo_grid,expression,the_alias,B2G(Field) field, bp3doc_field_BRIEF_F(field, NULL) as field_grid', 'ViewName' => 'bp3qry_column', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3qry_columnid) as bp3qry_columnid, B2G(bp3qry_columnid) as id,B2G(instanceid) as instanceid, bp3qry_column_BRIEF_F(bp3qry_columnid , NULL) as  brief,name,B2G(FromPart) frompart, bp3doc_store_BRIEF_F(frompart, NULL) as frompart_grid,aggregation, case aggregation  when 0 then \'none\' when 1 then \'AVG\' when 2 then \'COUNT\' when 3 then \'SUM\' when 4 then \'MIN\' when 5 then \'MAX\' when 6 then \'CUSTOM\' else \'\'  end   as aggregation_grid,sequence,forcombo, case forcombo  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as forcombo_grid,expression,the_alias,B2G(Field) field, bp3doc_field_BRIEF_F(field, NULL) as field_grid', 'ViewName' => 'bp3qry_column', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3qry_column', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>