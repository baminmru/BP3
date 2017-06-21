﻿
<?php
class  M_bp3ft_map extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3ft_mapid) as bp3ft_mapid, B2G(bp3ft_mapid) as id,B2G(instanceid) as instanceid, bp3ft_map_BRIEF_F(bp3ft_mapid , NULL) as  brief,B2G(Target) target, bp3dic_gen_BRIEF_F(target, NULL) as target_grid,fixedsize,stoagetype', 'PartName' => 'bp3ft_map', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3ft_mapid'])) {
	        $data['bp3ft_mapid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3ft_map', 'RowID' => $data['bp3ft_mapid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3ft_map', 'RowID' => $data['bp3ft_mapid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3ft_mapid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3ft_map', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3ft_mapid) as bp3ft_mapid, B2G(bp3ft_mapid) as id,B2G(instanceid) as instanceid, bp3ft_map_BRIEF_F(bp3ft_mapid , NULL) as  brief,B2G(Target) target, bp3dic_gen_BRIEF_F(target, NULL) as target_grid,fixedsize,stoagetype', 'ViewName' => 'bp3ft_map'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3ft_mapid) as bp3ft_mapid, B2G(bp3ft_mapid) as id,B2G(instanceid) as instanceid, bp3ft_map_BRIEF_F(bp3ft_mapid , NULL) as  brief,B2G(Target) target, bp3dic_gen_BRIEF_F(target, NULL) as target_grid,fixedsize,stoagetype', 'ViewName' => 'bp3ft_map', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3ft_mapid) as bp3ft_mapid, B2G(bp3ft_mapid) as id,B2G(instanceid) as instanceid, bp3ft_map_BRIEF_F(bp3ft_mapid , NULL) as  brief,B2G(Target) target, bp3dic_gen_BRIEF_F(target, NULL) as target_grid,fixedsize,stoagetype', 'ViewName' => 'bp3ft_map', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3ft_map', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>