
<?php
class  M_v_autoexcar_info extends CI_Model {
    public function  __construct()
    {
         parent::__construct();
    }
    function newRow($objtype,$name)  {
               $id = $this->jservice->get(array('Action'=>'NewGuid'));
               $this->jservice->get(array('Action'=>'NewDocument', 'TypeName'=>'exCar', 'DocumentID'=>$id, 'DocumentCaption'=>$name));
               $rowid = $this->jservice->get(array('Action'=>'NewGuid'));
               $this->jservice->get(array('Action'=>'NewRow', 'PartName'=>'excar_info', 'RowID'=>$rowid, 'DocumentID'=>$id));
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
			  case 'excar_info_theyear_le':
			  $cond = 'excar_info_theyear<='.$obj->value;
			  break;
			  case 'excar_info_theyear_ge':
			  $cond = 'excar_info_theyear>='.$obj->value;
			  break;
			  case 'excar_info_kasko_le':
			  $cond = 'excar_info_kasko<="'.$obj->value.'"';
			  break;
			  case 'excar_info_kasko_ge':
			  $cond = 'excar_info_kasko>="'.$obj->value.'"';
			  break;
			  case 'excar_info_osago_le':
			  $cond = 'excar_info_osago<="'.$obj->value.'"';
			  break;
			  case 'excar_info_osago_ge':
			  $cond = 'excar_info_osago>="'.$obj->value.'"';
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
	    $res = $this->jservice->get(array('Action' => 'GetViewData', 'ViewName' => 'v_autoexcar_info','FieldList'=>'instanceid,id,excar_info_theyear,excar_info_thecolor,excar_info_passport,excar_info_talon,DATE_FORMAT(excar_info_kasko,\'%Y-%m-%d\') excar_info_kasko,excar_info_carmarkdic,excar_info_vin,DATE_FORMAT(excar_info_osago,\'%Y-%m-%d\') excar_info_osago,excar_info_carnum','Sort'=>$sort, 'WhereClause' => $whereclause,'Limit'=>$limit,'Offset'=>$offset));
	} else {
	    $res = $this->jservice->get(array('Action' => 'GetViewData', 'ViewName' => 'v_autoexcar_info','FieldList'=>'instanceid,id,excar_info_theyear,excar_info_thecolor,excar_info_passport,excar_info_talon,DATE_FORMAT(excar_info_kasko,\'%Y-%m-%d\') excar_info_kasko,excar_info_carmarkdic,excar_info_vin,DATE_FORMAT(excar_info_osago,\'%Y-%m-%d\') excar_info_osago,excar_info_carnum','Sort'=>$sort, 'WhereClause' => $whereclause));
	}
	$root = new stdClass();
	$root->total = $this->jservice->get(array('Action' => 'CountView', 'ViewName' => 'v_autoexcar_info','FieldList'=>'instanceid,id,excar_info_theyear,excar_info_thecolor,excar_info_passport,excar_info_talon,DATE_FORMAT(excar_info_kasko,\'%Y-%m-%d\') excar_info_kasko,excar_info_carmarkdic,excar_info_vin,DATE_FORMAT(excar_info_osago,\'%Y-%m-%d\') excar_info_osago,excar_info_carnum', 'WhereClause' => $whereclause));
	$root->success = true;
	$root->rows = $res;
	return $root;
}
    function deleteRow($id = null) {
	    $this->jservice->get(array('Action'=>'DeleteDocument', 'TypeName'=>'excar', 'DocumentID'=>$id));
	    $return = array('success' => true);
	    return $return;
    }
}
?>