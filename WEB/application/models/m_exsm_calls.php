
<?php
class  M_exsm_calls extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(exsm_callsid) as exsm_callsid, B2G(exsm_callsid) as id,B2G(instanceid) as instanceid, exSm_calls_BRIEF_F(exsm_callsid , NULL) as  brief,  DATE_FORMAT(calltime,\'%H:%i:%s\') as  calltime,theaddress,clientphone,B2G(theSrc) thesrc, exSrc_info_BRIEF_F(thesrc, NULL) as thesrc_grid,cost,beznal, case beznal  when -1 then \'Да\' when 0 then \'Нет\'End  as beznal_grid,B2G(zstate) zstate, exDic_zstate_BRIEF_F(zstate, NULL) as zstate_grid,predvar, case predvar  when -1 then \'Да\' when 0 then \'Нет\'End  as predvar_grid,  DATE_FORMAT(totime,\'%H:%i:%s\') as  totime,drivercomment,clientcomment', 'PartName' => 'exsm_calls', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['exsm_callsid'])) {
	        $data['exsm_callsid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'exsm_calls', 'RowID' => $data['exsm_callsid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'exsm_calls', 'RowID' => $data['exsm_callsid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['exsm_callsid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'exsm_calls', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(exsm_callsid) as exsm_callsid, B2G(exsm_callsid) as id,B2G(instanceid) as instanceid, exSm_calls_BRIEF_F(exsm_callsid , NULL) as  brief,  DATE_FORMAT(calltime,\'%H:%i:%s\') as  calltime,theaddress,clientphone,B2G(theSrc) thesrc, exSrc_info_BRIEF_F(thesrc, NULL) as thesrc_grid,cost,beznal, case beznal  when -1 then \'Да\' when 0 then \'Нет\'End  as beznal_grid,B2G(zstate) zstate, exDic_zstate_BRIEF_F(zstate, NULL) as zstate_grid,predvar, case predvar  when -1 then \'Да\' when 0 then \'Нет\'End  as predvar_grid,  DATE_FORMAT(totime,\'%H:%i:%s\') as  totime,drivercomment,clientcomment', 'ViewName' => 'exsm_calls'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(exsm_callsid) as exsm_callsid, B2G(exsm_callsid) as id,B2G(instanceid) as instanceid, exSm_calls_BRIEF_F(exsm_callsid , NULL) as  brief,  DATE_FORMAT(calltime,\'%H:%i:%s\') as  calltime,theaddress,clientphone,B2G(theSrc) thesrc, exSrc_info_BRIEF_F(thesrc, NULL) as thesrc_grid,cost,beznal, case beznal  when -1 then \'Да\' when 0 then \'Нет\'End  as beznal_grid,B2G(zstate) zstate, exDic_zstate_BRIEF_F(zstate, NULL) as zstate_grid,predvar, case predvar  when -1 then \'Да\' when 0 then \'Нет\'End  as predvar_grid,  DATE_FORMAT(totime,\'%H:%i:%s\') as  totime,drivercomment,clientcomment', 'ViewName' => 'exsm_calls', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(exsm_callsid) as exsm_callsid, B2G(exsm_callsid) as id,B2G(instanceid) as instanceid, exSm_calls_BRIEF_F(exsm_callsid , NULL) as  brief,  DATE_FORMAT(calltime,\'%H:%i:%s\') as  calltime,theaddress,clientphone,B2G(theSrc) thesrc, exSrc_info_BRIEF_F(thesrc, NULL) as thesrc_grid,cost,beznal, case beznal  when -1 then \'Да\' when 0 then \'Нет\'End  as beznal_grid,B2G(zstate) zstate, exDic_zstate_BRIEF_F(zstate, NULL) as zstate_grid,predvar, case predvar  when -1 then \'Да\' when 0 then \'Нет\'End  as predvar_grid,  DATE_FORMAT(totime,\'%H:%i:%s\') as  totime,drivercomment,clientcomment', 'ViewName' => 'exsm_calls', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'exsm_calls', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>