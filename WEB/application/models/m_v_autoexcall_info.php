
<?php
class  M_v_autoexcall_info extends CI_Model {
    public function  __construct()
    {
         parent::__construct();
    }
    function newRow($objtype,$name)  {
               $id = $this->jservice->get(array('Action'=>'NewGuid'));
               $this->jservice->get(array('Action'=>'NewDocument', 'TypeName'=>'exCall', 'DocumentID'=>$id, 'DocumentCaption'=>$name));
               $rowid = $this->jservice->get(array('Action'=>'NewGuid'));
               $this->jservice->get(array('Action'=>'NewRow', 'PartName'=>'excall_info', 'RowID'=>$rowid, 'DocumentID'=>$id));
                if ($id) {
                    return array('success'=>TRUE, 'data' => $id, 'rowid'=>$rowid);
                }
                else {
                    $return= array('success'=>FALSE, 'msg' => 'Error while create new id');
				    return $return;
                }
    }
      function getRowsSL($offset,$limit, $sort = array(), $filter = null){
        $filter = (array)json_decode($filter);
       	$cond ='';
        $whereclause = '';
    try{
	    foreach($filter as $obj) {
		    $tmp = json_decode($obj->value);
		    if(is_array($tmp)) $obj->value = $tmp;	
		    switch($obj->property) {
			    //case 'value':
			    	//$cond = '';
			    	//break;
			  case 'excall_info_totime_le':
			  $cond = 'excall_info_totime<="'.$obj->value.'"';
			  break;
			  case 'excall_info_totime_ge':
			  $cond = 'excall_info_totime>="'.$obj->value.'"';
			  break;
			  case 'excall_info_calltime_le':
			  $cond = 'excall_info_calltime<="'.$obj->value.'"';
			  break;
			  case 'excall_info_calltime_ge':
			  $cond = 'excall_info_calltime>="'.$obj->value.'"';
			  break;
			  case 'excall_info_cost_le':
			  $cond = 'excall_info_cost<='.$obj->value;
			  break;
			  case 'excall_info_cost_ge':
			  $cond = 'excall_info_cost>='.$obj->value;
			  break;
		    	default:
			    	if(isset($obj->value))
			    	{
			    		if(is_array($obj->value))
				    	{
				    		$cond = $obj->property . ' IN ("' . implode('", "',$obj->value).'") ';
				    		//echo $cond;
					    }else
					    {
					    	$cond = $obj->property . ' LIKE "%' . $obj->value . '%"';
					    }
				    }else{
				        $cond='1=1';
				    }
		    }
		    $whereclause .= (empty($whereclause)) ? $cond : ' AND ' . $cond;
	    }
    }catch(Exception $e) {
	    log_message('error','Exception: '. $e->getMessage());
    }
	 if (isset($offset) && isset($limit)) {
	    $res = $this->jservice->get(array('Action' => 'GetViewData', 'ViewName' => 'v_autoexcall_info','FieldList'=>'instanceid,id,DATE_FORMAT(excall_info_totime,\' %H:%i:%s\') excall_info_totime,excall_info_beznal,excall_info_drivercomment,DATE_FORMAT(excall_info_calltime,\' %H:%i:%s\') excall_info_calltime,excall_info_clientphone,excall_info_thedriver,excall_info_thesrc,excall_info_zstate,excall_info_theaddress,excall_info_cost,excall_info_clientcomment,excall_info_predvar','Sort'=>$sort, 'WhereClause' => $whereclause,'Limit'=>$limit,'Offset'=>$offset));
	} else {
	    $res = $this->jservice->get(array('Action' => 'GetViewData', 'ViewName' => 'v_autoexcall_info','FieldList'=>'instanceid,id,DATE_FORMAT(excall_info_totime,\' %H:%i:%s\') excall_info_totime,excall_info_beznal,excall_info_drivercomment,DATE_FORMAT(excall_info_calltime,\' %H:%i:%s\') excall_info_calltime,excall_info_clientphone,excall_info_thedriver,excall_info_thesrc,excall_info_zstate,excall_info_theaddress,excall_info_cost,excall_info_clientcomment,excall_info_predvar','Sort'=>$sort, 'WhereClause' => $whereclause));
	}
	$root = new stdClass();
	$root->total = $this->jservice->get(array('Action' => 'CountView', 'ViewName' => 'v_autoexcall_info','FieldList'=>'instanceid,id,DATE_FORMAT(excall_info_totime,\' %H:%i:%s\') excall_info_totime,excall_info_beznal,excall_info_drivercomment,DATE_FORMAT(excall_info_calltime,\' %H:%i:%s\') excall_info_calltime,excall_info_clientphone,excall_info_thedriver,excall_info_thesrc,excall_info_zstate,excall_info_theaddress,excall_info_cost,excall_info_clientcomment,excall_info_predvar', 'WhereClause' => $whereclause));
	$root->success = true;
	$root->rows = $res;
	return $root;
}
    function deleteRow($id = null) {
	    $this->jservice->get(array('Action'=>'DeleteDocument', 'TypeName'=>'excall', 'DocumentID'=>$id));
	    $return = array('success' => true);
	    return $return;
    }
}
?>