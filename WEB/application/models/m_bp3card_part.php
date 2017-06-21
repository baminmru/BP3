
<?php
class  M_bp3card_part extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3card_partid) as bp3card_partid, B2G(bp3card_partid) as id,B2G(instanceid) as instanceid, bp3card_part_BRIEF_F(bp3card_partid , NULL) as  brief,sequence,B2G(Struct) struct, bp3card_part_BRIEF_F(struct, NULL) as struct_grid,allowedit, case allowedit  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowedit_grid,allowread, case allowread  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowread_grid,allowdelete, case allowdelete  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowdelete_grid,allowadd, case allowadd  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowadd_grid,caption', 'PartName' => 'bp3card_part', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
	if (!empty($data)) {
	    if (empty($data['bp3card_partid'])) {
	        $data['bp3card_partid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3card_part', 'RowID' => $data['bp3card_partid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3card_part', 'RowID' => $data['bp3card_partid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3card_partid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3card_part', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3card_partid) as bp3card_partid, B2G(bp3card_partid) as id,B2G(instanceid) as instanceid, bp3card_part_BRIEF_F(bp3card_partid , NULL) as  brief,sequence,B2G(Struct) struct, bp3card_part_BRIEF_F(struct, NULL) as struct_grid,allowedit, case allowedit  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowedit_grid,allowread, case allowread  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowread_grid,allowdelete, case allowdelete  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowdelete_grid,allowadd, case allowadd  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowadd_grid,caption', 'ViewName' => 'bp3card_part'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3card_partid) as bp3card_partid, B2G(bp3card_partid) as id,B2G(instanceid) as instanceid, bp3card_part_BRIEF_F(bp3card_partid , NULL) as  brief,sequence,B2G(Struct) struct, bp3card_part_BRIEF_F(struct, NULL) as struct_grid,allowedit, case allowedit  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowedit_grid,allowread, case allowread  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowread_grid,allowdelete, case allowdelete  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowdelete_grid,allowadd, case allowadd  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowadd_grid,caption', 'ViewName' => 'bp3card_part', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3card_partid) as bp3card_partid, B2G(bp3card_partid) as id,B2G(instanceid) as instanceid, bp3card_part_BRIEF_F(bp3card_partid , NULL) as  brief,sequence,B2G(Struct) struct, bp3card_part_BRIEF_F(struct, NULL) as struct_grid,allowedit, case allowedit  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowedit_grid,allowread, case allowread  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowread_grid,allowdelete, case allowdelete  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowdelete_grid,allowadd, case allowadd  when -1 then \'Да\' when 0 then \'Нет\' else \'\'  end   as allowadd_grid,caption', 'ViewName' => 'bp3card_part', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3card_part', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>