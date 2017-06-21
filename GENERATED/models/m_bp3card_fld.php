
<?php
class  M_bp3card_fld extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3card_fldid) as bp3card_fldid, B2G(bp3card_fldid) as id,B2G(instanceid) as instanceid, bp3card_fld_BRIEF_F(bp3card_fldid , NULL) as  brief,fieldgroupbox,sequence,B2G(ThePart) thepart, bp3card_part_BRIEF_F(thepart, NULL) as thepart_grid,allowmodify, case allowmodify  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowmodify_grid,B2G(TheField) thefield, bp3card_fld_BRIEF_F(thefield, NULL) as thefield_grid,tabname,thestyle,caption,allowread, case allowread  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowread_grid,mandatoryfield, case mandatoryfield  when -1 then \'Не существенно\' when 0 then \'Нет\' when 1 then \'Да\' else \'\'  end   as mandatoryfield_grid', 'PartName' => 'bp3card_fld', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3card_fldid'])) {
	        $data['bp3card_fldid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3card_fld', 'RowID' => $data['bp3card_fldid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3card_fld', 'RowID' => $data['bp3card_fldid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3card_fldid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3card_fld', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3card_fldid) as bp3card_fldid, B2G(bp3card_fldid) as id,B2G(instanceid) as instanceid, bp3card_fld_BRIEF_F(bp3card_fldid , NULL) as  brief,fieldgroupbox,sequence,B2G(ThePart) thepart, bp3card_part_BRIEF_F(thepart, NULL) as thepart_grid,allowmodify, case allowmodify  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowmodify_grid,B2G(TheField) thefield, bp3card_fld_BRIEF_F(thefield, NULL) as thefield_grid,tabname,thestyle,caption,allowread, case allowread  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowread_grid,mandatoryfield, case mandatoryfield  when -1 then \'Не существенно\' when 0 then \'Нет\' when 1 then \'Да\' else \'\'  end   as mandatoryfield_grid', 'ViewName' => 'bp3card_fld'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3card_fldid) as bp3card_fldid, B2G(bp3card_fldid) as id,B2G(instanceid) as instanceid, bp3card_fld_BRIEF_F(bp3card_fldid , NULL) as  brief,fieldgroupbox,sequence,B2G(ThePart) thepart, bp3card_part_BRIEF_F(thepart, NULL) as thepart_grid,allowmodify, case allowmodify  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowmodify_grid,B2G(TheField) thefield, bp3card_fld_BRIEF_F(thefield, NULL) as thefield_grid,tabname,thestyle,caption,allowread, case allowread  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowread_grid,mandatoryfield, case mandatoryfield  when -1 then \'Не существенно\' when 0 then \'Нет\' when 1 then \'Да\' else \'\'  end   as mandatoryfield_grid', 'ViewName' => 'bp3card_fld', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3card_fldid) as bp3card_fldid, B2G(bp3card_fldid) as id,B2G(instanceid) as instanceid, bp3card_fld_BRIEF_F(bp3card_fldid , NULL) as  brief,fieldgroupbox,sequence,B2G(ThePart) thepart, bp3card_part_BRIEF_F(thepart, NULL) as thepart_grid,allowmodify, case allowmodify  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowmodify_grid,B2G(TheField) thefield, bp3card_fld_BRIEF_F(thefield, NULL) as thefield_grid,tabname,thestyle,caption,allowread, case allowread  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowread_grid,mandatoryfield, case mandatoryfield  when -1 then \'Не существенно\' when 0 then \'Нет\' when 1 then \'Да\' else \'\'  end   as mandatoryfield_grid', 'ViewName' => 'bp3card_fld', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3card_fld', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>