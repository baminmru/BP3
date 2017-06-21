<?php
	 class C_bp3qry_column extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3qry_column.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3qry_column.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3qry_columnid' =>  $this->input->get_post('bp3qry_columnid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'forcombo' =>   $this->input->get_post('forcombo', TRUE)
                ,'frompart' =>   $this->input->get_post('frompart', TRUE)
                ,'aggregation' =>   $this->input->get_post('aggregation', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'the_alias' =>   $this->input->get_post('the_alias', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'field' =>   $this->input->get_post('field', TRUE)
                ,'expression' =>   $this->input->get_post('expression', TRUE)
            );
            $bp3qry_column = $this->m_bp3qry_column->setRow($data);
            print json_encode($bp3qry_column);
    }
    function newRow() {
            log_message('debug', 'bp3qry_column.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3qry_columnid' =>  $this->input->get_post('bp3qry_columnid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'forcombo' =>   $this->input->get_post('forcombo', TRUE)
                ,'frompart' =>   $this->input->get_post('frompart', TRUE)
                ,'aggregation' =>   $this->input->get_post('aggregation', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'the_alias' =>   $this->input->get_post('the_alias', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'field' =>   $this->input->get_post('field', TRUE)
                ,'expression' =>   $this->input->get_post('expression', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $bp3qry_column= $this->m_bp3qry_column->newRow($instanceid,$data);
            $return = $bp3qry_column;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3qry_column.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3qry_column = $this->m_bp3qry_column->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3qry_column
            );
            print json_encode($return);
        }
    }
    function getRows() {
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'sequence', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
            $instanceid=$this->input->get('instanceid', FALSE);
            if(isset($instanceid)){
                if($instanceid!=''){
                    $bp3qry_column= $this->m_bp3qry_column->getRowsByInstance($instanceid,$sort);
                }else{
                    $bp3qry_column= $this->m_bp3qry_column->getRows($sort);
                }
            }else{
              $bp3qry_column= $this->m_bp3qry_column->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3qry_column
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3qry_column.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'sequence', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $InstId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($InstId) > 0) {
            $bp3qry_column= $this->m_bp3qry_column->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3qry_column
            );
        }
        else {
            $return = array(
                'success' => FALSE,
                'msg'     => 'Need instanceid to query.'
            );
        }
        print json_encode($return);
    }
    function getRowsByParent() {
        log_message('debug', 'bp3qry_column.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'sequence', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $ParentId  =  $this->input->get_post('parentid', TRUE);
        if (strlen($ParentId) > 0) {
            $bp3qry_column= $this->m_bp3qry_column->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3qry_column
            );
        }
        else {
            $return = array(
                'success' => FALSE,
                'msg'     => 'Need parent parentid to query.'
            );
        }
        print json_encode($return);
    }
    function deleteRow() {
        log_message('debug', 'bp3qry_column.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3qry_columnid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3qry_column->deleteRow($tempId);
        }
        else {
            $return = array(
                'success' => FALSE,
                'msg'     => 'No  ID to delete'
            );
        }
        print json_encode($return);
    }
    private function _loadModels () {
        $this->load->model('M_bp3qry_column', 'm_bp3qry_column');
    }
}
?>