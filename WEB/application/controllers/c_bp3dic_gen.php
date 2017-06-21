<?php
	 class C_bp3dic_gen extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3dic_gen.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3dic_gen.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3dic_genid' =>  $this->input->get_post('bp3dic_genid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'generatorstyle' =>   $this->input->get_post('generatorstyle', TRUE)
                ,'queuename' =>   $this->input->get_post('queuename', TRUE)
                ,'thedevelopmentenv' =>   $this->input->get_post('thedevelopmentenv', TRUE)
                ,'generatorprogid' =>   $this->input->get_post('generatorprogid', TRUE)
                ,'targettype' =>   $this->input->get_post('targettype', TRUE)
            );
            $bp3dic_gen = $this->m_bp3dic_gen->setRow($data);
            print json_encode($bp3dic_gen);
    }
    function newRow() {
            log_message('debug', 'bp3dic_gen.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3dic_genid' =>  $this->input->get_post('bp3dic_genid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'generatorstyle' =>   $this->input->get_post('generatorstyle', TRUE)
                ,'queuename' =>   $this->input->get_post('queuename', TRUE)
                ,'thedevelopmentenv' =>   $this->input->get_post('thedevelopmentenv', TRUE)
                ,'generatorprogid' =>   $this->input->get_post('generatorprogid', TRUE)
                ,'targettype' =>   $this->input->get_post('targettype', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $bp3dic_gen= $this->m_bp3dic_gen->newRow($instanceid,$data);
            $return = $bp3dic_gen;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3dic_gen.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3dic_gen = $this->m_bp3dic_gen->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3dic_gen
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
                    $bp3dic_gen= $this->m_bp3dic_gen->getRowsByInstance($instanceid,$sort);
                }else{
                    $bp3dic_gen= $this->m_bp3dic_gen->getRows($sort);
                }
            }else{
              $bp3dic_gen= $this->m_bp3dic_gen->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3dic_gen
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3dic_gen.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3dic_gen= $this->m_bp3dic_gen->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3dic_gen
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
        log_message('debug', 'bp3dic_gen.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3dic_gen= $this->m_bp3dic_gen->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3dic_gen
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
        log_message('debug', 'bp3dic_gen.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3dic_genid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3dic_gen->deleteRow($tempId);
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
        $this->load->model('M_bp3dic_gen', 'm_bp3dic_gen');
    }
}
?>