<?php
	 class C_bp3list_col extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3list_col.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3list_col.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3list_colid' =>  $this->input->get_post('bp3list_colid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'viewfield' =>   $this->input->get_post('viewfield', TRUE)
                ,'colwidth' =>   $this->input->get_post('colwidth', TRUE)
                ,'columnalignment' =>   $this->input->get_post('columnalignment', TRUE)
                ,'colsort' =>   $this->input->get_post('colsort', TRUE)
                ,'thestyle' =>   $this->input->get_post('thestyle', TRUE)
                ,'groupaggregation' =>   $this->input->get_post('groupaggregation', TRUE)
            );
            $bp3list_col = $this->m_bp3list_col->setRow($data);
            print json_encode($bp3list_col);
    }
    function newRow() {
            log_message('debug', 'bp3list_col.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3list_colid' =>  $this->input->get_post('bp3list_colid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'viewfield' =>   $this->input->get_post('viewfield', TRUE)
                ,'colwidth' =>   $this->input->get_post('colwidth', TRUE)
                ,'columnalignment' =>   $this->input->get_post('columnalignment', TRUE)
                ,'colsort' =>   $this->input->get_post('colsort', TRUE)
                ,'thestyle' =>   $this->input->get_post('thestyle', TRUE)
                ,'groupaggregation' =>   $this->input->get_post('groupaggregation', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $bp3list_col= $this->m_bp3list_col->newRow($instanceid,$data);
            $return = $bp3list_col;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3list_col.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3list_col = $this->m_bp3list_col->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3list_col
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
                    $bp3list_col= $this->m_bp3list_col->getRowsByInstance($instanceid,$sort);
                }else{
                    $bp3list_col= $this->m_bp3list_col->getRows($sort);
                }
            }else{
              $bp3list_col= $this->m_bp3list_col->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3list_col
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3list_col.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3list_col= $this->m_bp3list_col->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3list_col
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
        log_message('debug', 'bp3list_col.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3list_col= $this->m_bp3list_col->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3list_col
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
        log_message('debug', 'bp3list_col.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3list_colid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3list_col->deleteRow($tempId);
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
        $this->load->model('M_bp3list_col', 'm_bp3list_col');
    }
}
?>