<?php
	 class C_bp3report_filterfiel extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3report_filterfiel.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3report_filterfiel.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3report_filterfielid' =>  $this->input->get_post('bp3report_filterfielid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'reftopart' =>   $this->input->get_post('reftopart', TRUE)
                ,'reftype' =>   $this->input->get_post('reftype', TRUE)
                ,'valuearray' =>   $this->input->get_post('valuearray', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'fieldtype' =>   $this->input->get_post('fieldtype', TRUE)
                ,'fieldsize' =>   $this->input->get_post('fieldsize', TRUE)
            );
            $bp3report_filterfiel = $this->m_bp3report_filterfiel->setRow($data);
            print json_encode($bp3report_filterfiel);
    }
    function newRow() {
            log_message('debug', 'bp3report_filterfiel.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3report_filterfielid' =>  $this->input->get_post('bp3report_filterfielid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'reftopart' =>   $this->input->get_post('reftopart', TRUE)
                ,'reftype' =>   $this->input->get_post('reftype', TRUE)
                ,'valuearray' =>   $this->input->get_post('valuearray', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'fieldtype' =>   $this->input->get_post('fieldtype', TRUE)
                ,'fieldsize' =>   $this->input->get_post('fieldsize', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
                $parentid =  $this->input->get_post('parentid', TRUE);
            $bp3report_filterfiel= $this->m_bp3report_filterfiel->newRow($instanceid,$parentid,$data);
            $return = $bp3report_filterfiel;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3report_filterfiel.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3report_filterfiel = $this->m_bp3report_filterfiel->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3report_filterfiel
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
            $parentid=$this->input->get('parentid', FALSE);
            if(isset($parentid)){
                if($parentid!=''){
                    $bp3report_filterfiel= $this->m_bp3report_filterfiel->getRowsByParent($parentid,$sort);
                }else{
                    $bp3report_filterfiel= $this->m_bp3report_filterfiel->getRows($sort);
                }
            }else{
              $bp3report_filterfiel= $this->m_bp3report_filterfiel->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3report_filterfiel
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3report_filterfiel.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3report_filterfiel= $this->m_bp3report_filterfiel->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3report_filterfiel
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
        log_message('debug', 'bp3report_filterfiel.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3report_filterfiel= $this->m_bp3report_filterfiel->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3report_filterfiel
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
        log_message('debug', 'bp3report_filterfiel.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3report_filterfielid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3report_filterfiel->deleteRow($tempId);
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
        $this->load->model('M_bp3report_filterfiel', 'm_bp3report_filterfiel');
    }
}
?>