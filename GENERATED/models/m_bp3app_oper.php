
<?php
class  M_bp3app_oper extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3app_operid) as bp3app_operid, B2G(bp3app_operid) as id,B2G(parentstructrowid) as parentid, BP3APP_OPER_BRIEF_F(bp3app_operid , NULL) as  brief,name,caption,B2G(rightType) righttype, BP3APP_RIGTHTYPE_BRIEF_F(righttype, NULL) as righttype_grid,isreport, case isreport  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isreport_grid,sequence,theicon', 'PartName' => 'bp3app_oper', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3app_operid'])) {
	        $data['bp3app_operid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3app_oper', 'RowID' => $data['bp3app_operid'], 'DocumentID' => $data['instanceid'],'ParentID' => $data['parentid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3app_oper', 'RowID' => $data['bp3app_operid'], 'DocumentID' => $data['instanceid'],'ParentID' => $data['parentid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3app_operid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$parentid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3app_oper', 'RowID' => $id, 'DocumentID' => $instanceid,'ParentID'=>$parentid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3app_operid) as bp3app_operid, B2G(bp3app_operid) as id,B2G(parentstructrowid) as parentid, BP3APP_OPER_BRIEF_F(bp3app_operid , NULL) as  brief,name,caption,B2G(rightType) righttype, BP3APP_RIGTHTYPE_BRIEF_F(righttype, NULL) as righttype_grid,isreport, case isreport  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isreport_grid,sequence,theicon', 'ViewName' => 'bp3app_oper'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3app_operid) as bp3app_operid, B2G(bp3app_operid) as id,B2G(parentstructrowid) as parentid, BP3APP_OPER_BRIEF_F(bp3app_operid , NULL) as  brief,name,caption,B2G(rightType) righttype, BP3APP_RIGTHTYPE_BRIEF_F(righttype, NULL) as righttype_grid,isreport, case isreport  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isreport_grid,sequence,theicon', 'ViewName' => 'bp3app_oper', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3app_operid) as bp3app_operid, B2G(bp3app_operid) as id,B2G(parentstructrowid) as parentid, BP3APP_OPER_BRIEF_F(bp3app_operid , NULL) as  brief,name,caption,B2G(rightType) righttype, BP3APP_RIGTHTYPE_BRIEF_F(righttype, NULL) as righttype_grid,isreport, case isreport  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isreport_grid,sequence,theicon', 'ViewName' => 'bp3app_oper', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3app_oper', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>