
<?php
class  M_bp3report_filterfiel extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3report_filterfielid) as bp3report_filterfielid, B2G(bp3report_filterfielid) as id,B2G(parentstructrowid) as parentid, bp3report_filterfiel_BRIEF_F(bp3report_filterfielid , NULL) as  brief,sequence,reftype, case reftype  when 0 then \'Скалярное поле ( не ссылка)\' when 1 then \'На объект \' when 2 then \'На строку раздела\' when 3 then \'На источник данных\' else \'\'  end   as reftype_grid,name,B2G(RefToPart) reftopart, bp3doc_store_BRIEF_F(reftopart, NULL) as reftopart_grid,B2G(FieldType) fieldtype, bp3ft_def_BRIEF_F(fieldtype, NULL) as fieldtype_grid,caption,valuearray, case valuearray  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as valuearray_grid,fieldsize', 'PartName' => 'bp3report_filterfiel', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3report_filterfielid'])) {
	        $data['bp3report_filterfielid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3report_filterfiel', 'RowID' => $data['bp3report_filterfielid'], 'DocumentID' => $data['instanceid'],'ParentID' => $data['parentid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3report_filterfiel', 'RowID' => $data['bp3report_filterfielid'], 'DocumentID' => $data['instanceid'],'ParentID' => $data['parentid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3report_filterfielid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$parentid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3report_filterfiel', 'RowID' => $id, 'DocumentID' => $instanceid,'ParentID'=>$parentid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3report_filterfielid) as bp3report_filterfielid, B2G(bp3report_filterfielid) as id,B2G(parentstructrowid) as parentid, bp3report_filterfiel_BRIEF_F(bp3report_filterfielid , NULL) as  brief,sequence,reftype, case reftype  when 0 then \'Скалярное поле ( не ссылка)\' when 1 then \'На объект \' when 2 then \'На строку раздела\' when 3 then \'На источник данных\' else \'\'  end   as reftype_grid,name,B2G(RefToPart) reftopart, bp3doc_store_BRIEF_F(reftopart, NULL) as reftopart_grid,B2G(FieldType) fieldtype, bp3ft_def_BRIEF_F(fieldtype, NULL) as fieldtype_grid,caption,valuearray, case valuearray  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as valuearray_grid,fieldsize', 'ViewName' => 'bp3report_filterfiel'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3report_filterfielid) as bp3report_filterfielid, B2G(bp3report_filterfielid) as id,B2G(parentstructrowid) as parentid, bp3report_filterfiel_BRIEF_F(bp3report_filterfielid , NULL) as  brief,sequence,reftype, case reftype  when 0 then \'Скалярное поле ( не ссылка)\' when 1 then \'На объект \' when 2 then \'На строку раздела\' when 3 then \'На источник данных\' else \'\'  end   as reftype_grid,name,B2G(RefToPart) reftopart, bp3doc_store_BRIEF_F(reftopart, NULL) as reftopart_grid,B2G(FieldType) fieldtype, bp3ft_def_BRIEF_F(fieldtype, NULL) as fieldtype_grid,caption,valuearray, case valuearray  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as valuearray_grid,fieldsize', 'ViewName' => 'bp3report_filterfiel', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3report_filterfielid) as bp3report_filterfielid, B2G(bp3report_filterfielid) as id,B2G(parentstructrowid) as parentid, bp3report_filterfiel_BRIEF_F(bp3report_filterfielid , NULL) as  brief,sequence,reftype, case reftype  when 0 then \'Скалярное поле ( не ссылка)\' when 1 then \'На объект \' when 2 then \'На строку раздела\' when 3 then \'На источник данных\' else \'\'  end   as reftype_grid,name,B2G(RefToPart) reftopart, bp3doc_store_BRIEF_F(reftopart, NULL) as reftopart_grid,B2G(FieldType) fieldtype, bp3ft_def_BRIEF_F(fieldtype, NULL) as fieldtype_grid,caption,valuearray, case valuearray  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as valuearray_grid,fieldsize', 'ViewName' => 'bp3report_filterfiel', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3report_filterfiel', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>