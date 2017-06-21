
<?php
class  M_v_autoexsm_info extends CI_Model {
    public function  __construct()
    {
         parent::__construct();
    }
    function newRow($objtype,$name)  {
               $id = $this->jservice->get(array('Action'=>'NewGuid'));
               $this->jservice->get(array('Action'=>'NewDocument', 'TypeName'=>'exSm', 'DocumentID'=>$id, 'DocumentCaption'=>$name));
               $rowid = $this->jservice->get(array('Action'=>'NewGuid'));
               $this->jservice->get(array('Action'=>'NewRow', 'PartName'=>'exsm_info', 'RowID'=>$rowid, 'DocumentID'=>$id));
                if ($id) {
                    return array('success'=>TRUE, 'data' => $id, 'rowid'=>$rowid);
                }
                else {
                    $return= array('success'=>FALSE, 'msg' => 'Error while create new id');
				    return $return;
                }
    }
      function getRowsSL($offset,$limit, $sort = array(), $filter = null, $archived=0){
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
			  case 'exsm_info_start_balabns_le':
			  $cond = 'exsm_info_start_balabns<='.$obj->value;
			  break;
			  case 'exsm_info_start_balabns_ge':
			  $cond = 'exsm_info_start_balabns>='.$obj->value;
			  break;
			  case 'exsm_info_end_balans_le':
			  $cond = 'exsm_info_end_balans<='.$obj->value;
			  break;
			  case 'exsm_info_end_balans_ge':
			  $cond = 'exsm_info_end_balans>='.$obj->value;
			  break;
			  case 'exsm_info_thedate_le':
			  $cond = 'exsm_info_thedate<="'.$obj->value.'"';
			  break;
			  case 'exsm_info_thedate_ge':
			  $cond = 'exsm_info_thedate>="'.$obj->value.'"';
			  break;
			  case 'exsm_info_smnumber_le':
			  $cond = 'exsm_info_smnumber<='.$obj->value;
			  break;
			  case 'exsm_info_smnumber_ge':
			  $cond = 'exsm_info_smnumber>='.$obj->value;
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
	    $res = $this->jservice->get(array('Action' => 'GetViewData', 'ViewName' => 'v_autoexsm_info','FieldList'=>'instanceid,id,exsm_info_thedriver,exsm_info_thecar,exsm_info_start_balabns,exsm_info_end_balans,DATE_FORMAT(exsm_info_thedate,\'%Y-%m-%d\') exsm_info_thedate,exsm_info_smnumber,exsm_info_sclosed','Sort'=>$sort, 'WhereClause' => $whereclause,'Limit'=>$limit,'Offset'=>$offset,'archived'=>$archived));
	} else {
	    $res = $this->jservice->get(array('Action' => 'GetViewData', 'ViewName' => 'v_autoexsm_info','FieldList'=>'instanceid,id,exsm_info_thedriver,exsm_info_thecar,exsm_info_start_balabns,exsm_info_end_balans,DATE_FORMAT(exsm_info_thedate,\'%Y-%m-%d\') exsm_info_thedate,exsm_info_smnumber,exsm_info_sclosed','Sort'=>$sort, 'WhereClause' => $whereclause));
	}
	$root = new stdClass();
	$root->total = $this->jservice->get(array('Action' => 'CountView', 'ViewName' => 'v_autoexsm_info','FieldList'=>'instanceid,id,exsm_info_thedriver,exsm_info_thecar,exsm_info_start_balabns,exsm_info_end_balans,DATE_FORMAT(exsm_info_thedate,\'%Y-%m-%d\') exsm_info_thedate,exsm_info_smnumber,exsm_info_sclosed', 'WhereClause' => $whereclause,'archived'=>$archived));
	$root->success = true;
	$root->rows = $res;
	return $root;
}
    function deleteRow($id = null) {
	    $this->jservice->get(array('Action'=>'ArchiveDocument', 'TypeName'=>'exsm', 'DocumentID'=>$id));
	    $return = array('success' => true);
	    return $return;
    }
}
?>