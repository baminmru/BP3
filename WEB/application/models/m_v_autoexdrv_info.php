
<?php
class  M_v_autoexdrv_info extends CI_Model {
    public function  __construct()
    {
         parent::__construct();
    }
    function newRow($objtype,$name)  {
               $id = $this->jservice->get(array('Action'=>'NewGuid'));
               $this->jservice->get(array('Action'=>'NewDocument', 'TypeName'=>'exDrv', 'DocumentID'=>$id, 'DocumentCaption'=>$name));
               $rowid = $this->jservice->get(array('Action'=>'NewGuid'));
               $this->jservice->get(array('Action'=>'NewRow', 'PartName'=>'exdrv_info', 'RowID'=>$rowid, 'DocumentID'=>$id));
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
			  case 'exdrv_info_bdate_le':
			  $cond = 'exdrv_info_bdate<="'.$obj->value.'"';
			  break;
			  case 'exdrv_info_bdate_ge':
			  $cond = 'exdrv_info_bdate>="'.$obj->value.'"';
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
	    $res = $this->jservice->get(array('Action' => 'GetViewData', 'ViewName' => 'v_autoexdrv_info','FieldList'=>'instanceid,id,exdrv_info_homephone,exdrv_info_f_addr,exdrv_info_fulltime,exdrv_info_surname,exdrv_info_stage,exdrv_info_p_addr,exdrv_info_shortname,exdrv_info_lastname,exdrv_info_name,exdrv_info_phone,DATE_FORMAT(exdrv_info_bdate,\'%Y-%m-%d\') exdrv_info_bdate','Sort'=>$sort, 'WhereClause' => $whereclause,'Limit'=>$limit,'Offset'=>$offset,'archived'=>$archived));
	} else {
	    $res = $this->jservice->get(array('Action' => 'GetViewData', 'ViewName' => 'v_autoexdrv_info','FieldList'=>'instanceid,id,exdrv_info_homephone,exdrv_info_f_addr,exdrv_info_fulltime,exdrv_info_surname,exdrv_info_stage,exdrv_info_p_addr,exdrv_info_shortname,exdrv_info_lastname,exdrv_info_name,exdrv_info_phone,DATE_FORMAT(exdrv_info_bdate,\'%Y-%m-%d\') exdrv_info_bdate','Sort'=>$sort, 'WhereClause' => $whereclause));
	}
	$root = new stdClass();
	$root->total = $this->jservice->get(array('Action' => 'CountView', 'ViewName' => 'v_autoexdrv_info','FieldList'=>'instanceid,id,exdrv_info_homephone,exdrv_info_f_addr,exdrv_info_fulltime,exdrv_info_surname,exdrv_info_stage,exdrv_info_p_addr,exdrv_info_shortname,exdrv_info_lastname,exdrv_info_name,exdrv_info_phone,DATE_FORMAT(exdrv_info_bdate,\'%Y-%m-%d\') exdrv_info_bdate', 'WhereClause' => $whereclause,'archived'=>$archived));
	$root->success = true;
	$root->rows = $res;
	return $root;
}
    function deleteRow($id = null) {
	    $this->jservice->get(array('Action'=>'ArchiveDocument', 'TypeName'=>'exdrv', 'DocumentID'=>$id));
	    $return = array('success' => true);
	    return $return;
    }
}
?>