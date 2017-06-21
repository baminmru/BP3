<?php
	 class C_bp3app_modules extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'BP3APP_MODULES.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'BP3APP_MODULES.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3app_modulesid' =>  $this->input->get_post('bp3app_modulesid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'topmenu' =>   $this->input->get_post('topmenu', TRUE)
                ,'groupname' =>   $this->input->get_post('groupname', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'thecomment' =>   $this->input->get_post('thecomment', TRUE)
                ,'theicon' =>   $this->input->get_post('theicon', TRUE)
                ,'customizevisibility' =>   $this->input->get_post('customizevisibility', TRUE)
                ,'journal' =>   $this->input->get_post('journal', TRUE)
                ,'document' =>   $this->input->get_post('document', TRUE)
                ,'actiontype' =>   $this->input->get_post('actiontype', TRUE)
                ,'objecttype' =>   $this->input->get_post('objecttype', TRUE)
                ,'report' =>   $this->input->get_post('report', TRUE)
            );
            $bp3app_modules = $this->m_bp3app_modules->setRow($data);
            print json_encode($bp3app_modules);
    }
    function newRow() {
            log_message('debug', 'BP3APP_MODULES.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3app_modulesid' =>  $this->input->get_post('bp3app_modulesid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'topmenu' =>   $this->input->get_post('topmenu', TRUE)
                ,'groupname' =>   $this->input->get_post('groupname', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'thecomment' =>   $this->input->get_post('thecomment', TRUE)
                ,'theicon' =>   $this->input->get_post('theicon', TRUE)
                ,'customizevisibility' =>   $this->input->get_post('customizevisibility', TRUE)
                ,'journal' =>   $this->input->get_post('journal', TRUE)
                ,'document' =>   $this->input->get_post('document', TRUE)
                ,'actiontype' =>   $this->input->get_post('actiontype', TRUE)
                ,'objecttype' =>   $this->input->get_post('objecttype', TRUE)
                ,'report' =>   $this->input->get_post('report', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $bp3app_modules= $this->m_bp3app_modules->newRow($instanceid,$data);
            $return = $bp3app_modules;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'BP3APP_MODULES.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3app_modules = $this->m_bp3app_modules->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3app_modules
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
                    $bp3app_modules= $this->m_bp3app_modules->getRowsByInstance($instanceid,$sort);
                }else{
                    $bp3app_modules= $this->m_bp3app_modules->getRows($sort);
                }
            }else{
              $bp3app_modules= $this->m_bp3app_modules->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3app_modules
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'BP3APP_MODULES.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3app_modules= $this->m_bp3app_modules->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3app_modules
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
        log_message('debug', 'BP3APP_MODULES.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3app_modules= $this->m_bp3app_modules->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3app_modules
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
        log_message('debug', 'BP3APP_MODULES.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3app_modulesid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3app_modules->deleteRow($tempId);
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
        $this->load->model('M_bp3app_modules', 'm_bp3app_modules');
    }
}
?>