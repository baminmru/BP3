
<?php
class  M_bp3dic_gen extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3dic_genid) as bp3dic_genid, B2G(bp3dic_genid) as id,B2G(instanceid) as instanceid, bp3dic_gen_BRIEF_F(bp3dic_genid , NULL) as  brief,generatorstyle, case generatorstyle  when 0 then \'Один тип\' when 1 then \'Все типы сразу\' else \'\'  end   as generatorstyle_grid,name,queuename,targettype, case targettype  when 0 then \'СУБД\' when 1 then \'МОДЕЛЬ\' when 2 then \'Приложение\' when 3 then \'Документация\' when 4 then \'АРМ\' else \'\'  end   as targettype_grid,generatorprogid,thedevelopmentenv, case thedevelopmentenv  when 0 then \'VB6\' when 1 then \'DOTNET\' when 2 then \'JAVA\' when 3 then \'OTHER\' else \'\'  end   as thedevelopmentenv_grid', 'PartName' => 'bp3dic_gen', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3dic_genid'])) {
	        $data['bp3dic_genid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3dic_gen', 'RowID' => $data['bp3dic_genid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3dic_gen', 'RowID' => $data['bp3dic_genid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3dic_genid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3dic_gen', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3dic_genid) as bp3dic_genid, B2G(bp3dic_genid) as id,B2G(instanceid) as instanceid, bp3dic_gen_BRIEF_F(bp3dic_genid , NULL) as  brief,generatorstyle, case generatorstyle  when 0 then \'Один тип\' when 1 then \'Все типы сразу\' else \'\'  end   as generatorstyle_grid,name,queuename,targettype, case targettype  when 0 then \'СУБД\' when 1 then \'МОДЕЛЬ\' when 2 then \'Приложение\' when 3 then \'Документация\' when 4 then \'АРМ\' else \'\'  end   as targettype_grid,generatorprogid,thedevelopmentenv, case thedevelopmentenv  when 0 then \'VB6\' when 1 then \'DOTNET\' when 2 then \'JAVA\' when 3 then \'OTHER\' else \'\'  end   as thedevelopmentenv_grid', 'ViewName' => 'bp3dic_gen'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3dic_genid) as bp3dic_genid, B2G(bp3dic_genid) as id,B2G(instanceid) as instanceid, bp3dic_gen_BRIEF_F(bp3dic_genid , NULL) as  brief,generatorstyle, case generatorstyle  when 0 then \'Один тип\' when 1 then \'Все типы сразу\' else \'\'  end   as generatorstyle_grid,name,queuename,targettype, case targettype  when 0 then \'СУБД\' when 1 then \'МОДЕЛЬ\' when 2 then \'Приложение\' when 3 then \'Документация\' when 4 then \'АРМ\' else \'\'  end   as targettype_grid,generatorprogid,thedevelopmentenv, case thedevelopmentenv  when 0 then \'VB6\' when 1 then \'DOTNET\' when 2 then \'JAVA\' when 3 then \'OTHER\' else \'\'  end   as thedevelopmentenv_grid', 'ViewName' => 'bp3dic_gen', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3dic_genid) as bp3dic_genid, B2G(bp3dic_genid) as id,B2G(instanceid) as instanceid, bp3dic_gen_BRIEF_F(bp3dic_genid , NULL) as  brief,generatorstyle, case generatorstyle  when 0 then \'Один тип\' when 1 then \'Все типы сразу\' else \'\'  end   as generatorstyle_grid,name,queuename,targettype, case targettype  when 0 then \'СУБД\' when 1 then \'МОДЕЛЬ\' when 2 then \'Приложение\' when 3 then \'Документация\' when 4 then \'АРМ\' else \'\'  end   as targettype_grid,generatorprogid,thedevelopmentenv, case thedevelopmentenv  when 0 then \'VB6\' when 1 then \'DOTNET\' when 2 then \'JAVA\' when 3 then \'OTHER\' else \'\'  end   as thedevelopmentenv_grid', 'ViewName' => 'bp3dic_gen', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3dic_gen', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>