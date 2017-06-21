
<?php
class  M_bp3list_col extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3list_colid) as bp3list_colid, B2G(bp3list_colid) as id,B2G(instanceid) as instanceid, bp3list_col_BRIEF_F(bp3list_colid , NULL) as  brief,colsort, case colsort  when 0 then \'As String\' when 1 then \'As Numeric\' when 2 then \'As Date\' else \'\'  end   as colsort_grid,sequence,columnalignment, case columnalignment  when 0 then \'Left Top\' when 1 then \'Left Center\' when 2 then \'Left Bottom\' when 3 then \'Center Top\' when 4 then \'Center Center\' when 5 then \'Center Bottom\' when 6 then \'Right Top\' when 7 then \'Right Center\' when 8 then \'Right Bottom\' else \'\'  end   as columnalignment_grid,colwidth,thestyle,groupaggregation, case groupaggregation  when 0 then \'none\' when 1 then \'AVG\' when 2 then \'COUNT\' when 3 then \'SUM\' when 4 then \'MIN\' when 5 then \'MAX\' when 6 then \'CUSTOM\' else \'\'  end   as groupaggregation_grid,viewfield,name', 'PartName' => 'bp3list_col', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3list_colid'])) {
	        $data['bp3list_colid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3list_col', 'RowID' => $data['bp3list_colid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3list_col', 'RowID' => $data['bp3list_colid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3list_colid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3list_col', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3list_colid) as bp3list_colid, B2G(bp3list_colid) as id,B2G(instanceid) as instanceid, bp3list_col_BRIEF_F(bp3list_colid , NULL) as  brief,colsort, case colsort  when 0 then \'As String\' when 1 then \'As Numeric\' when 2 then \'As Date\' else \'\'  end   as colsort_grid,sequence,columnalignment, case columnalignment  when 0 then \'Left Top\' when 1 then \'Left Center\' when 2 then \'Left Bottom\' when 3 then \'Center Top\' when 4 then \'Center Center\' when 5 then \'Center Bottom\' when 6 then \'Right Top\' when 7 then \'Right Center\' when 8 then \'Right Bottom\' else \'\'  end   as columnalignment_grid,colwidth,thestyle,groupaggregation, case groupaggregation  when 0 then \'none\' when 1 then \'AVG\' when 2 then \'COUNT\' when 3 then \'SUM\' when 4 then \'MIN\' when 5 then \'MAX\' when 6 then \'CUSTOM\' else \'\'  end   as groupaggregation_grid,viewfield,name', 'ViewName' => 'bp3list_col'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3list_colid) as bp3list_colid, B2G(bp3list_colid) as id,B2G(instanceid) as instanceid, bp3list_col_BRIEF_F(bp3list_colid , NULL) as  brief,colsort, case colsort  when 0 then \'As String\' when 1 then \'As Numeric\' when 2 then \'As Date\' else \'\'  end   as colsort_grid,sequence,columnalignment, case columnalignment  when 0 then \'Left Top\' when 1 then \'Left Center\' when 2 then \'Left Bottom\' when 3 then \'Center Top\' when 4 then \'Center Center\' when 5 then \'Center Bottom\' when 6 then \'Right Top\' when 7 then \'Right Center\' when 8 then \'Right Bottom\' else \'\'  end   as columnalignment_grid,colwidth,thestyle,groupaggregation, case groupaggregation  when 0 then \'none\' when 1 then \'AVG\' when 2 then \'COUNT\' when 3 then \'SUM\' when 4 then \'MIN\' when 5 then \'MAX\' when 6 then \'CUSTOM\' else \'\'  end   as groupaggregation_grid,viewfield,name', 'ViewName' => 'bp3list_col', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3list_colid) as bp3list_colid, B2G(bp3list_colid) as id,B2G(instanceid) as instanceid, bp3list_col_BRIEF_F(bp3list_colid , NULL) as  brief,colsort, case colsort  when 0 then \'As String\' when 1 then \'As Numeric\' when 2 then \'As Date\' else \'\'  end   as colsort_grid,sequence,columnalignment, case columnalignment  when 0 then \'Left Top\' when 1 then \'Left Center\' when 2 then \'Left Bottom\' when 3 then \'Center Top\' when 4 then \'Center Center\' when 5 then \'Center Bottom\' when 6 then \'Right Top\' when 7 then \'Right Center\' when 8 then \'Right Bottom\' else \'\'  end   as columnalignment_grid,colwidth,thestyle,groupaggregation, case groupaggregation  when 0 then \'none\' when 1 then \'AVG\' when 2 then \'COUNT\' when 3 then \'SUM\' when 4 then \'MIN\' when 5 then \'MAX\' when 6 then \'CUSTOM\' else \'\'  end   as groupaggregation_grid,viewfield,name', 'ViewName' => 'bp3list_col', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3list_col', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>