<?php
	 class C_bp3app_menu extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'BP3APP_MENU.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'BP3APP_MENU.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3app_menuid' =>  $this->input->get_post('bp3app_menuid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'theicon' =>   $this->input->get_post('theicon', TRUE)
            );
            $bp3app_menu = $this->m_bp3app_menu->setRow($data);
            print json_encode($bp3app_menu);
    }
    function newRow() {
            log_message('debug', 'BP3APP_MENU.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3app_menuid' =>  $this->input->get_post('bp3app_menuid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'theicon' =>   $this->input->get_post('theicon', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $bp3app_menu= $this->m_bp3app_menu->newRow($instanceid,$data);
            $return = $bp3app_menu;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'BP3APP_MENU.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3app_menu = $this->m_bp3app_menu->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3app_menu
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
                    $bp3app_menu= $this->m_bp3app_menu->getRowsByInstance($instanceid,$sort);
                }else{
                    $bp3app_menu= $this->m_bp3app_menu->getRows($sort);
                }
            }else{
              $bp3app_menu= $this->m_bp3app_menu->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3app_menu
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'BP3APP_MENU.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3app_menu= $this->m_bp3app_menu->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3app_menu
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
        log_message('debug', 'BP3APP_MENU.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3app_menu= $this->m_bp3app_menu->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3app_menu
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
        log_message('debug', 'BP3APP_MENU.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3app_menuid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3app_menu->deleteRow($tempId);
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
        $this->load->model('M_bp3app_menu', 'm_bp3app_menu');
    }
}
?>