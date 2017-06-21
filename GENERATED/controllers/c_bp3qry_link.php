<?php
	 class C_bp3qry_link extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3qry_link.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3qry_link.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3qry_linkid' =>  $this->input->get_post('bp3qry_linkid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'thejoindestination' =>   $this->input->get_post('thejoindestination', TRUE)
                ,'handjoin' =>   $this->input->get_post('handjoin', TRUE)
                ,'seq' =>   $this->input->get_post('seq', TRUE)
                ,'thejoinsource' =>   $this->input->get_post('thejoinsource', TRUE)
                ,'theview' =>   $this->input->get_post('theview', TRUE)
                ,'reftype' =>   $this->input->get_post('reftype', TRUE)
            );
            $bp3qry_link = $this->m_bp3qry_link->setRow($data);
            print json_encode($bp3qry_link);
    }
    function newRow() {
            log_message('debug', 'bp3qry_link.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3qry_linkid' =>  $this->input->get_post('bp3qry_linkid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'thejoindestination' =>   $this->input->get_post('thejoindestination', TRUE)
                ,'handjoin' =>   $this->input->get_post('handjoin', TRUE)
                ,'seq' =>   $this->input->get_post('seq', TRUE)
                ,'thejoinsource' =>   $this->input->get_post('thejoinsource', TRUE)
                ,'theview' =>   $this->input->get_post('theview', TRUE)
                ,'reftype' =>   $this->input->get_post('reftype', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $bp3qry_link= $this->m_bp3qry_link->newRow($instanceid,$data);
            $return = $bp3qry_link;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3qry_link.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3qry_link = $this->m_bp3qry_link->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3qry_link
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
            	$sort[] = array('property'=>'theview', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
            $instanceid=$this->input->get('instanceid', FALSE);
            if(isset($instanceid)){
                if($instanceid!=''){
                    $bp3qry_link= $this->m_bp3qry_link->getRowsByInstance($instanceid,$sort);
                }else{
                    $bp3qry_link= $this->m_bp3qry_link->getRows($sort);
                }
            }else{
              $bp3qry_link= $this->m_bp3qry_link->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3qry_link
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3qry_link.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'theview', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $InstId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($InstId) > 0) {
            $bp3qry_link= $this->m_bp3qry_link->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3qry_link
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
        log_message('debug', 'bp3qry_link.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'theview', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $ParentId  =  $this->input->get_post('parentid', TRUE);
        if (strlen($ParentId) > 0) {
            $bp3qry_link= $this->m_bp3qry_link->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3qry_link
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
        log_message('debug', 'bp3qry_link.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3qry_linkid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3qry_link->deleteRow($tempId);
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
        $this->load->model('M_bp3qry_link', 'm_bp3qry_link');
    }
}
?>