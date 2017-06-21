
<?php
class  M_bp3report_filter extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3report_filterid) as bp3report_filterid, B2G(bp3report_filterid) as id,B2G(instanceid) as instanceid, bp3report_filter_BRIEF_F(bp3report_filterid , NULL) as  brief,name,allowignore, case allowignore  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowignore_grid,caption,sequence', 'PartName' => 'bp3report_filter', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3report_filterid'])) {
	        $data['bp3report_filterid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3report_filter', 'RowID' => $data['bp3report_filterid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3report_filter', 'RowID' => $data['bp3report_filterid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3report_filterid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3report_filter', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3report_filterid) as bp3report_filterid, B2G(bp3report_filterid) as id,B2G(instanceid) as instanceid, bp3report_filter_BRIEF_F(bp3report_filterid , NULL) as  brief,name,allowignore, case allowignore  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowignore_grid,caption,sequence', 'ViewName' => 'bp3report_filter'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3report_filterid) as bp3report_filterid, B2G(bp3report_filterid) as id,B2G(instanceid) as instanceid, bp3report_filter_BRIEF_F(bp3report_filterid , NULL) as  brief,name,allowignore, case allowignore  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowignore_grid,caption,sequence', 'ViewName' => 'bp3report_filter', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3report_filterid) as bp3report_filterid, B2G(bp3report_filterid) as id,B2G(instanceid) as instanceid, bp3report_filter_BRIEF_F(bp3report_filterid , NULL) as  brief,name,allowignore, case allowignore  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowignore_grid,caption,sequence', 'ViewName' => 'bp3report_filter', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3report_filter', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>