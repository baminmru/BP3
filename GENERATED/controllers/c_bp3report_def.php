<?php
	 class C_bp3report_def extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3report_def.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3report_def.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3report_defid' =>  $this->input->get_post('bp3report_defid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'reporttype' =>   $this->input->get_post('reporttype', TRUE)
                ,'thecomment' =>   $this->input->get_post('thecomment', TRUE)
                ,'reportfile' =>   $this->input->get_post('reportfile', TRUE)
                ,'reportfile_ext' =>   $this->input->get_post('reportfile_ext', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'reportview' =>   $this->input->get_post('reportview', TRUE)
            );
            $bp3report_def = $this->m_bp3report_def->setRow($data);
            print json_encode($bp3report_def);
    }
    function newRow() {
            log_message('debug', 'bp3report_def.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3report_defid' =>  $this->input->get_post('bp3report_defid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'reporttype' =>   $this->input->get_post('reporttype', TRUE)
                ,'thecomment' =>   $this->input->get_post('thecomment', TRUE)
                ,'reportfile' =>   $this->input->get_post('reportfile', TRUE)
                ,'reportfile_ext' =>   $this->input->get_post('reportfile_ext', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'reportview' =>   $this->input->get_post('reportview', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $bp3report_def= $this->m_bp3report_def->newRow($instanceid,$data);
            $return = $bp3report_def;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3report_def.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3report_def = $this->m_bp3report_def->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3report_def
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
            	$sort[] = array('property'=>'name', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
            $instanceid=$this->input->get('instanceid', FALSE);
            if(isset($instanceid)){
                if($instanceid!=''){
                    $bp3report_def= $this->m_bp3report_def->getRowsByInstance($instanceid,$sort);
                }else{
                    $bp3report_def= $this->m_bp3report_def->getRows($sort);
                }
            }else{
              $bp3report_def= $this->m_bp3report_def->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3report_def
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3report_def.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'name', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $InstId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($InstId) > 0) {
            $bp3report_def= $this->m_bp3report_def->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3report_def
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
        log_message('debug', 'bp3report_def.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'name', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $ParentId  =  $this->input->get_post('parentid', TRUE);
        if (strlen($ParentId) > 0) {
            $bp3report_def= $this->m_bp3report_def->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3report_def
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
        log_message('debug', 'bp3report_def.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3report_defid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3report_def->deleteRow($tempId);
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
        $this->load->model('M_bp3report_def', 'm_bp3report_def');
    }
}
?>