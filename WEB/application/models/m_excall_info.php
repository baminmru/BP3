
<?php
class  M_excall_info extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(excall_infoid) as excall_infoid, B2G(excall_infoid) as id,B2G(instanceid) as instanceid, exCall_info_BRIEF_F(excall_infoid , NULL) as  brief,  DATE_FORMAT(calltime,\'%H:%i:%s\') as  calltime,B2G(zstate) zstate, exDic_zstate_BRIEF_F(zstate, NULL) as zstate_grid,B2G(theDriver) thedriver, exDrv_info_BRIEF_F(thedriver, NULL) as thedriver_grid,predvar, case predvar  when -1 then \'Да\' when 0 then \'Нет\'End  as predvar_grid,  DATE_FORMAT(totime,\'%H:%i:%s\') as  totime,theaddress,clientphone,B2G(theSrc) thesrc, exSrc_info_BRIEF_F(thesrc, NULL) as thesrc_grid,beznal, case beznal  when -1 then \'Да\' when 0 then \'Нет\'End  as beznal_grid,cost,clientcomment,drivercomment', 'PartName' => 'excall_info', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['excall_infoid'])) {
	        $data['excall_infoid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'excall_info', 'RowID' => $data['excall_infoid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'excall_info', 'RowID' => $data['excall_infoid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['excall_infoid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'excall_info', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(excall_infoid) as excall_infoid, B2G(excall_infoid) as id,B2G(instanceid) as instanceid, exCall_info_BRIEF_F(excall_infoid , NULL) as  brief,  DATE_FORMAT(calltime,\'%H:%i:%s\') as  calltime,B2G(zstate) zstate, exDic_zstate_BRIEF_F(zstate, NULL) as zstate_grid,B2G(theDriver) thedriver, exDrv_info_BRIEF_F(thedriver, NULL) as thedriver_grid,predvar, case predvar  when -1 then \'Да\' when 0 then \'Нет\'End  as predvar_grid,  DATE_FORMAT(totime,\'%H:%i:%s\') as  totime,theaddress,clientphone,B2G(theSrc) thesrc, exSrc_info_BRIEF_F(thesrc, NULL) as thesrc_grid,beznal, case beznal  when -1 then \'Да\' when 0 then \'Нет\'End  as beznal_grid,cost,clientcomment,drivercomment', 'ViewName' => 'excall_info'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(excall_infoid) as excall_infoid, B2G(excall_infoid) as id,B2G(instanceid) as instanceid, exCall_info_BRIEF_F(excall_infoid , NULL) as  brief,  DATE_FORMAT(calltime,\'%H:%i:%s\') as  calltime,B2G(zstate) zstate, exDic_zstate_BRIEF_F(zstate, NULL) as zstate_grid,B2G(theDriver) thedriver, exDrv_info_BRIEF_F(thedriver, NULL) as thedriver_grid,predvar, case predvar  when -1 then \'Да\' when 0 then \'Нет\'End  as predvar_grid,  DATE_FORMAT(totime,\'%H:%i:%s\') as  totime,theaddress,clientphone,B2G(theSrc) thesrc, exSrc_info_BRIEF_F(thesrc, NULL) as thesrc_grid,beznal, case beznal  when -1 then \'Да\' when 0 then \'Нет\'End  as beznal_grid,cost,clientcomment,drivercomment', 'ViewName' => 'excall_info', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(excall_infoid) as excall_infoid, B2G(excall_infoid) as id,B2G(instanceid) as instanceid, exCall_info_BRIEF_F(excall_infoid , NULL) as  brief,  DATE_FORMAT(calltime,\'%H:%i:%s\') as  calltime,B2G(zstate) zstate, exDic_zstate_BRIEF_F(zstate, NULL) as zstate_grid,B2G(theDriver) thedriver, exDrv_info_BRIEF_F(thedriver, NULL) as thedriver_grid,predvar, case predvar  when -1 then \'Да\' when 0 then \'Нет\'End  as predvar_grid,  DATE_FORMAT(totime,\'%H:%i:%s\') as  totime,theaddress,clientphone,B2G(theSrc) thesrc, exSrc_info_BRIEF_F(thesrc, NULL) as thesrc_grid,beznal, case beznal  when -1 then \'Да\' when 0 then \'Нет\'End  as beznal_grid,cost,clientcomment,drivercomment', 'ViewName' => 'excall_info', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'excall_info', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>