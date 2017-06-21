
<?php
class  M_bp3list_filterfield extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3list_filterfieldid) as bp3list_filterfieldid, B2G(bp3list_filterfieldid) as id,B2G(parentstructrowid) as parentid, bp3list_filterfield_BRIEF_F(bp3list_filterfieldid , NULL) as  brief,B2G(RefToPart) reftopart, bp3doc_store_BRIEF_F(reftopart, NULL) as reftopart_grid,reftype, case reftype  when 0 then \'Скалярное поле ( не ссылка)\' when 1 then \'На объект \' when 2 then \'На строку раздела\' when 3 then \'На источник данных\' else \'\'  end   as reftype_grid,valuearray, case valuearray  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as valuearray_grid,sequence,caption,name,B2G(FieldType) fieldtype, bp3ft_def_BRIEF_F(fieldtype, NULL) as fieldtype_grid,fieldsize', 'PartName' => 'bp3list_filterfield', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3list_filterfieldid'])) {
	        $data['bp3list_filterfieldid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3list_filterfield', 'RowID' => $data['bp3list_filterfieldid'], 'DocumentID' => $data['instanceid'],'ParentID' => $data['parentid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3list_filterfield', 'RowID' => $data['bp3list_filterfieldid'], 'DocumentID' => $data['instanceid'],'ParentID' => $data['parentid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3list_filterfieldid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$parentid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3list_filterfield', 'RowID' => $id, 'DocumentID' => $instanceid,'ParentID'=>$parentid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3list_filterfieldid) as bp3list_filterfieldid, B2G(bp3list_filterfieldid) as id,B2G(parentstructrowid) as parentid, bp3list_filterfield_BRIEF_F(bp3list_filterfieldid , NULL) as  brief,B2G(RefToPart) reftopart, bp3doc_store_BRIEF_F(reftopart, NULL) as reftopart_grid,reftype, case reftype  when 0 then \'Скалярное поле ( не ссылка)\' when 1 then \'На объект \' when 2 then \'На строку раздела\' when 3 then \'На источник данных\' else \'\'  end   as reftype_grid,valuearray, case valuearray  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as valuearray_grid,sequence,caption,name,B2G(FieldType) fieldtype, bp3ft_def_BRIEF_F(fieldtype, NULL) as fieldtype_grid,fieldsize', 'ViewName' => 'bp3list_filterfield'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3list_filterfieldid) as bp3list_filterfieldid, B2G(bp3list_filterfieldid) as id,B2G(parentstructrowid) as parentid, bp3list_filterfield_BRIEF_F(bp3list_filterfieldid , NULL) as  brief,B2G(RefToPart) reftopart, bp3doc_store_BRIEF_F(reftopart, NULL) as reftopart_grid,reftype, case reftype  when 0 then \'Скалярное поле ( не ссылка)\' when 1 then \'На объект \' when 2 then \'На строку раздела\' when 3 then \'На источник данных\' else \'\'  end   as reftype_grid,valuearray, case valuearray  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as valuearray_grid,sequence,caption,name,B2G(FieldType) fieldtype, bp3ft_def_BRIEF_F(fieldtype, NULL) as fieldtype_grid,fieldsize', 'ViewName' => 'bp3list_filterfield', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3list_filterfieldid) as bp3list_filterfieldid, B2G(bp3list_filterfieldid) as id,B2G(parentstructrowid) as parentid, bp3list_filterfield_BRIEF_F(bp3list_filterfieldid , NULL) as  brief,B2G(RefToPart) reftopart, bp3doc_store_BRIEF_F(reftopart, NULL) as reftopart_grid,reftype, case reftype  when 0 then \'Скалярное поле ( не ссылка)\' when 1 then \'На объект \' when 2 then \'На строку раздела\' when 3 then \'На источник данных\' else \'\'  end   as reftype_grid,valuearray, case valuearray  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as valuearray_grid,sequence,caption,name,B2G(FieldType) fieldtype, bp3ft_def_BRIEF_F(fieldtype, NULL) as fieldtype_grid,fieldsize', 'ViewName' => 'bp3list_filterfield', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3list_filterfield', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>