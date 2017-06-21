
<?php
class  M_bp3app_modules extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3app_modulesid) as bp3app_modulesid, B2G(bp3app_modulesid) as id,B2G(instanceid) as instanceid, BP3APP_MODULES_BRIEF_F(bp3app_modulesid , NULL) as  brief,B2G(Report) report, bp3report_def_BRIEF_F(report, NULL) as report_grid,thecomment,B2G(TopMenu) topmenu, BP3APP_MENU_BRIEF_F(topmenu, NULL) as topmenu_grid,name,customizevisibility, case customizevisibility  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as customizevisibility_grid,actiontype, case actiontype  when 0 then \'Ничего не делать\' when 1 then \'Открыть документ\' when 2 then \'Выполнить метод\' when 3 then \'Открыть журнал\' when 4 then \'Запустить АРМ\' when 5 then \'Открыть отчет\' else \'\'  end   as actiontype_grid,caption,theicon,B2G(ObjectType) objecttype, bp3doc_def_BRIEF_F(objecttype, NULL) as objecttype_grid,B2G(Journal) journal, bp3list_def_BRIEF_F(journal, NULL) as journal_grid,sequence,document,groupname', 'PartName' => 'bp3app_modules', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3app_modulesid'])) {
	        $data['bp3app_modulesid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3app_modules', 'RowID' => $data['bp3app_modulesid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3app_modules', 'RowID' => $data['bp3app_modulesid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3app_modulesid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3app_modules', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3app_modulesid) as bp3app_modulesid, B2G(bp3app_modulesid) as id,B2G(instanceid) as instanceid, BP3APP_MODULES_BRIEF_F(bp3app_modulesid , NULL) as  brief,B2G(Report) report, bp3report_def_BRIEF_F(report, NULL) as report_grid,thecomment,B2G(TopMenu) topmenu, BP3APP_MENU_BRIEF_F(topmenu, NULL) as topmenu_grid,name,customizevisibility, case customizevisibility  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as customizevisibility_grid,actiontype, case actiontype  when 0 then \'Ничего не делать\' when 1 then \'Открыть документ\' when 2 then \'Выполнить метод\' when 3 then \'Открыть журнал\' when 4 then \'Запустить АРМ\' when 5 then \'Открыть отчет\' else \'\'  end   as actiontype_grid,caption,theicon,B2G(ObjectType) objecttype, bp3doc_def_BRIEF_F(objecttype, NULL) as objecttype_grid,B2G(Journal) journal, bp3list_def_BRIEF_F(journal, NULL) as journal_grid,sequence,document,groupname', 'ViewName' => 'bp3app_modules'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3app_modulesid) as bp3app_modulesid, B2G(bp3app_modulesid) as id,B2G(instanceid) as instanceid, BP3APP_MODULES_BRIEF_F(bp3app_modulesid , NULL) as  brief,B2G(Report) report, bp3report_def_BRIEF_F(report, NULL) as report_grid,thecomment,B2G(TopMenu) topmenu, BP3APP_MENU_BRIEF_F(topmenu, NULL) as topmenu_grid,name,customizevisibility, case customizevisibility  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as customizevisibility_grid,actiontype, case actiontype  when 0 then \'Ничего не делать\' when 1 then \'Открыть документ\' when 2 then \'Выполнить метод\' when 3 then \'Открыть журнал\' when 4 then \'Запустить АРМ\' when 5 then \'Открыть отчет\' else \'\'  end   as actiontype_grid,caption,theicon,B2G(ObjectType) objecttype, bp3doc_def_BRIEF_F(objecttype, NULL) as objecttype_grid,B2G(Journal) journal, bp3list_def_BRIEF_F(journal, NULL) as journal_grid,sequence,document,groupname', 'ViewName' => 'bp3app_modules', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3app_modulesid) as bp3app_modulesid, B2G(bp3app_modulesid) as id,B2G(instanceid) as instanceid, BP3APP_MODULES_BRIEF_F(bp3app_modulesid , NULL) as  brief,B2G(Report) report, bp3report_def_BRIEF_F(report, NULL) as report_grid,thecomment,B2G(TopMenu) topmenu, BP3APP_MENU_BRIEF_F(topmenu, NULL) as topmenu_grid,name,customizevisibility, case customizevisibility  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as customizevisibility_grid,actiontype, case actiontype  when 0 then \'Ничего не делать\' when 1 then \'Открыть документ\' when 2 then \'Выполнить метод\' when 3 then \'Открыть журнал\' when 4 then \'Запустить АРМ\' when 5 then \'Открыть отчет\' else \'\'  end   as actiontype_grid,caption,theicon,B2G(ObjectType) objecttype, bp3doc_def_BRIEF_F(objecttype, NULL) as objecttype_grid,B2G(Journal) journal, bp3list_def_BRIEF_F(journal, NULL) as journal_grid,sequence,document,groupname', 'ViewName' => 'bp3app_modules', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3app_modules', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>