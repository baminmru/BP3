
<?php
class  M_exdic_zstate extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(exdic_zstateid) as exdic_zstateid, B2G(exdic_zstateid) as id,B2G(instanceid) as instanceid, exDic_zstate_BRIEF_F(exdic_zstateid , NULL) as  brief,name,isfinal, case isfinal  when -1 then \'Да\' when 0 then \'Нет\'End  as isfinal_grid', 'PartName' => 'exdic_zstate', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['exdic_zstateid'])) {
	        $data['exdic_zstateid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'exdic_zstate', 'RowID' => $data['exdic_zstateid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'exdic_zstate', 'RowID' => $data['exdic_zstateid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['exdic_zstateid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'exdic_zstate', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(exdic_zstateid) as exdic_zstateid, B2G(exdic_zstateid) as id,B2G(instanceid) as instanceid, exDic_zstate_BRIEF_F(exdic_zstateid , NULL) as  brief,name,isfinal, case isfinal  when -1 then \'Да\' when 0 then \'Нет\'End  as isfinal_grid', 'ViewName' => 'exdic_zstate'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(exdic_zstateid) as exdic_zstateid, B2G(exdic_zstateid) as id,B2G(instanceid) as instanceid, exDic_zstate_BRIEF_F(exdic_zstateid , NULL) as  brief,name,isfinal, case isfinal  when -1 then \'Да\' when 0 then \'Нет\'End  as isfinal_grid', 'ViewName' => 'exdic_zstate', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(exdic_zstateid) as exdic_zstateid, B2G(exdic_zstateid) as id,B2G(instanceid) as instanceid, exDic_zstate_BRIEF_F(exdic_zstateid , NULL) as  brief,name,isfinal, case isfinal  when -1 then \'Да\' when 0 then \'Нет\'End  as isfinal_grid', 'ViewName' => 'exdic_zstate', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'exdic_zstate', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>