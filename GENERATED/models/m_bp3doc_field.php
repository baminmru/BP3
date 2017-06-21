
<?php
class  M_bp3doc_field extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3doc_fieldid) as bp3doc_fieldid, B2G(bp3doc_fieldid) as id,B2G(parentstructrowid) as parentid, bp3doc_field_BRIEF_F(bp3doc_fieldid , NULL) as  brief,name,fieldgroupbox,datasize,thestyle,tabname,themask,isautonumber, case isautonumber  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isautonumber_grid,thecomment,caption,istabbrief, case istabbrief  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as istabbrief_grid,sequence,isbrief, case isbrief  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isbrief_grid,B2G(FieldType) fieldtype, bp3ft_def_BRIEF_F(fieldtype, NULL) as fieldtype_grid,referencetype, case referencetype  when 0 then \'Скалярное поле ( не ссылка)\' when 1 then \'На объект \' when 2 then \'На строку раздела\' when 3 then \'На источник данных\' else \'\'  end   as referencetype_grid,B2G(RefToPart) reftopart, bp3doc_store_BRIEF_F(reftopart, NULL) as reftopart_grid,internalreference, case internalreference  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as internalreference_grid,allownull, case allownull  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allownull_grid,shablonbrief', 'PartName' => 'bp3doc_field', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3doc_fieldid'])) {
	        $data['bp3doc_fieldid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3doc_field', 'RowID' => $data['bp3doc_fieldid'], 'DocumentID' => $data['instanceid'],'ParentID' => $data['parentid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3doc_field', 'RowID' => $data['bp3doc_fieldid'], 'DocumentID' => $data['instanceid'],'ParentID' => $data['parentid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3doc_fieldid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$parentid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3doc_field', 'RowID' => $id, 'DocumentID' => $instanceid,'ParentID'=>$parentid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_fieldid) as bp3doc_fieldid, B2G(bp3doc_fieldid) as id,B2G(parentstructrowid) as parentid, bp3doc_field_BRIEF_F(bp3doc_fieldid , NULL) as  brief,name,fieldgroupbox,datasize,thestyle,tabname,themask,isautonumber, case isautonumber  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isautonumber_grid,thecomment,caption,istabbrief, case istabbrief  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as istabbrief_grid,sequence,isbrief, case isbrief  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isbrief_grid,B2G(FieldType) fieldtype, bp3ft_def_BRIEF_F(fieldtype, NULL) as fieldtype_grid,referencetype, case referencetype  when 0 then \'Скалярное поле ( не ссылка)\' when 1 then \'На объект \' when 2 then \'На строку раздела\' when 3 then \'На источник данных\' else \'\'  end   as referencetype_grid,B2G(RefToPart) reftopart, bp3doc_store_BRIEF_F(reftopart, NULL) as reftopart_grid,internalreference, case internalreference  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as internalreference_grid,allownull, case allownull  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allownull_grid,shablonbrief', 'ViewName' => 'bp3doc_field'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_fieldid) as bp3doc_fieldid, B2G(bp3doc_fieldid) as id,B2G(parentstructrowid) as parentid, bp3doc_field_BRIEF_F(bp3doc_fieldid , NULL) as  brief,name,fieldgroupbox,datasize,thestyle,tabname,themask,isautonumber, case isautonumber  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isautonumber_grid,thecomment,caption,istabbrief, case istabbrief  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as istabbrief_grid,sequence,isbrief, case isbrief  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isbrief_grid,B2G(FieldType) fieldtype, bp3ft_def_BRIEF_F(fieldtype, NULL) as fieldtype_grid,referencetype, case referencetype  when 0 then \'Скалярное поле ( не ссылка)\' when 1 then \'На объект \' when 2 then \'На строку раздела\' when 3 then \'На источник данных\' else \'\'  end   as referencetype_grid,B2G(RefToPart) reftopart, bp3doc_store_BRIEF_F(reftopart, NULL) as reftopart_grid,internalreference, case internalreference  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as internalreference_grid,allownull, case allownull  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allownull_grid,shablonbrief', 'ViewName' => 'bp3doc_field', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3doc_fieldid) as bp3doc_fieldid, B2G(bp3doc_fieldid) as id,B2G(parentstructrowid) as parentid, bp3doc_field_BRIEF_F(bp3doc_fieldid , NULL) as  brief,name,fieldgroupbox,datasize,thestyle,tabname,themask,isautonumber, case isautonumber  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isautonumber_grid,thecomment,caption,istabbrief, case istabbrief  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as istabbrief_grid,sequence,isbrief, case isbrief  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as isbrief_grid,B2G(FieldType) fieldtype, bp3ft_def_BRIEF_F(fieldtype, NULL) as fieldtype_grid,referencetype, case referencetype  when 0 then \'Скалярное поле ( не ссылка)\' when 1 then \'На объект \' when 2 then \'На строку раздела\' when 3 then \'На источник данных\' else \'\'  end   as referencetype_grid,B2G(RefToPart) reftopart, bp3doc_store_BRIEF_F(reftopart, NULL) as reftopart_grid,internalreference, case internalreference  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as internalreference_grid,allownull, case allownull  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allownull_grid,shablonbrief', 'ViewName' => 'bp3doc_field', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3doc_field', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>