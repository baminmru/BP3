﻿
<?php
class  M_bp3doc_store extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3doc_storeid) as bp3doc_storeid, B2G(bp3doc_storeid) as id,B2G(instanceid) as instanceid,B2G(parentrowid) as parentrowid, bp3doc_store_BRIEF_F(bp3doc_storeid , NULL) as  brief,caption,sequence,shablonbrief,usearchiving, case usearchiving  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usearchiving_grid,nolog, case nolog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as nolog_grid,isdocinstance, case isdocinstance  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isdocinstance_grid,the_comment,rulebrief,parttype, case parttype  when 0 then \'Строка\' when 1 then \'Коллекция\' when 2 then \'Дерево\' when 3 then \'Расширение\' when 4 then \'Расширение с данными\' else \'\'  end   as parttype_grid,name,usechangelog, case usechangelog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usechangelog_grid', 'PartName' => 'bp3doc_store', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3doc_storeid'])) {
	        $data['bp3doc_storeid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3doc_store', 'RowID' => $data['bp3doc_storeid'], 'DocumentID' => $data['instanceid'],'TreeID'=>$data['treeid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3doc_store', 'RowID' => $data['bp3doc_storeid'], 'DocumentID' => $data['instanceid'],'TreeID'=>$data['treeid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3doc_storeid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$treeid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3doc_store', 'RowID' => $id, 'DocumentID' => $instanceid,'TreeID'=>$treeid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_storeid) as bp3doc_storeid, B2G(bp3doc_storeid) as id,B2G(instanceid) as instanceid,B2G(parentrowid) as parentrowid, bp3doc_store_BRIEF_F(bp3doc_storeid , NULL) as  brief,caption,sequence,shablonbrief,usearchiving, case usearchiving  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usearchiving_grid,nolog, case nolog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as nolog_grid,isdocinstance, case isdocinstance  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isdocinstance_grid,the_comment,rulebrief,parttype, case parttype  when 0 then \'Строка\' when 1 then \'Коллекция\' when 2 then \'Дерево\' when 3 then \'Расширение\' when 4 then \'Расширение с данными\' else \'\'  end   as parttype_grid,name,usechangelog, case usechangelog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usechangelog_grid', 'ViewName' => 'bp3doc_store'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_storeid) as bp3doc_storeid, B2G(bp3doc_storeid) as id,B2G(instanceid) as instanceid,B2G(parentrowid) as parentrowid, bp3doc_store_BRIEF_F(bp3doc_storeid , NULL) as  brief,caption,sequence,shablonbrief,usearchiving, case usearchiving  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usearchiving_grid,nolog, case nolog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as nolog_grid,isdocinstance, case isdocinstance  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isdocinstance_grid,the_comment,rulebrief,parttype, case parttype  when 0 then \'Строка\' when 1 then \'Коллекция\' when 2 then \'Дерево\' when 3 then \'Расширение\' when 4 then \'Расширение с данными\' else \'\'  end   as parttype_grid,name,usechangelog, case usechangelog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usechangelog_grid', 'ViewName' => 'bp3doc_store', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_storeid) as bp3doc_storeid, B2G(bp3doc_storeid) as id,B2G(instanceid) as instanceid,B2G(parentrowid) as parentrowid, bp3doc_store_BRIEF_F(bp3doc_storeid , NULL) as  brief,caption,sequence,shablonbrief,usearchiving, case usearchiving  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usearchiving_grid,nolog, case nolog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as nolog_grid,isdocinstance, case isdocinstance  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isdocinstance_grid,the_comment,rulebrief,parttype, case parttype  when 0 then \'Строка\' when 1 then \'Коллекция\' when 2 then \'Дерево\' when 3 then \'Расширение\' when 4 then \'Расширение с данными\' else \'\'  end   as parttype_grid,name,usechangelog, case usechangelog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usechangelog_grid', 'ViewName' => 'bp3doc_store', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function getRowsByTree($treeid,$sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_storeid) as bp3doc_storeid, B2G(bp3doc_storeid) as id,B2G(instanceid) as instanceid,B2G(parentrowid) as parentrowid, bp3doc_store_BRIEF_F(bp3doc_storeid , NULL) as  brief,caption,sequence,shablonbrief,usearchiving, case usearchiving  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usearchiving_grid,nolog, case nolog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as nolog_grid,isdocinstance, case isdocinstance  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isdocinstance_grid,the_comment,rulebrief,parttype, case parttype  when 0 then \'Строка\' when 1 then \'Коллекция\' when 2 then \'Дерево\' when 3 then \'Расширение\' when 4 then \'Расширение с данными\' else \'\'  end   as parttype_grid,name,usechangelog, case usechangelog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usechangelog_grid', 'ViewName' => 'bp3doc_store', 'WhereClause' => 'parentrowid=G2B(\''.$treeid.'\')'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstanceTree($id,$treeid,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_storeid) as bp3doc_storeid, B2G(bp3doc_storeid) as id,B2G(instanceid) as instanceid,B2G(parentrowid) as parentrowid, bp3doc_store_BRIEF_F(bp3doc_storeid , NULL) as  brief,caption,sequence,shablonbrief,usearchiving, case usearchiving  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usearchiving_grid,nolog, case nolog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as nolog_grid,isdocinstance, case isdocinstance  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isdocinstance_grid,the_comment,rulebrief,parttype, case parttype  when 0 then \'Строка\' when 1 then \'Коллекция\' when 2 then \'Дерево\' when 3 then \'Расширение\' when 4 then \'Расширение с данными\' else \'\'  end   as parttype_grid,name,usechangelog, case usechangelog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usechangelog_grid', 'ViewName' => 'bp3doc_store', 'WhereClause' => 'instanceid=G2B(\''. $id . '\') and parentrowid=G2B(\''.$treeid.'\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParentTree($id,$treeid,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_storeid) as bp3doc_storeid, B2G(bp3doc_storeid) as id,B2G(instanceid) as instanceid,B2G(parentrowid) as parentrowid, bp3doc_store_BRIEF_F(bp3doc_storeid , NULL) as  brief,caption,sequence,shablonbrief,usearchiving, case usearchiving  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usearchiving_grid,nolog, case nolog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as nolog_grid,isdocinstance, case isdocinstance  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isdocinstance_grid,the_comment,rulebrief,parttype, case parttype  when 0 then \'Строка\' when 1 then \'Коллекция\' when 2 then \'Дерево\' when 3 then \'Расширение\' when 4 then \'Расширение с данными\' else \'\'  end   as parttype_grid,name,usechangelog, case usechangelog  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as usechangelog_grid', 'ViewName' => 'bp3doc_store', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\') and parentrowid=G2B(\''.$treeid.'\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3doc_store', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>