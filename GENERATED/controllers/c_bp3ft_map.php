<?php
	 class C_bp3ft_map extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3ft_map.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3ft_map.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3ft_mapid' =>  $this->input->get_post('bp3ft_mapid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'fixedsize' =>   $this->input->get_post('fixedsize', TRUE)
                ,'stoagetype' =>   $this->input->get_post('stoagetype', TRUE)
                ,'target' =>   $this->input->get_post('target', TRUE)
            );
            $bp3ft_map = $this->m_bp3ft_map->setRow($data);
            print json_encode($bp3ft_map);
    }
    function newRow() {
            log_message('debug', 'bp3ft_map.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3ft_mapid' =>  $this->input->get_post('bp3ft_mapid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'fixedsize' =>   $this->input->get_post('fixedsize', TRUE)
                ,'stoagetype' =>   $this->input->get_post('stoagetype', TRUE)
                ,'target' =>   $this->input->get_post('target', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $bp3ft_map= $this->m_bp3ft_map->newRow($instanceid,$data);
            $return = $bp3ft_map;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3ft_map.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3ft_map = $this->m_bp3ft_map->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3ft_map
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
            	$sort[] = array('property'=>'stoagetype', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
            $instanceid=$this->input->get('instanceid', FALSE);
            if(isset($instanceid)){
                if($instanceid!=''){
                    $bp3ft_map= $this->m_bp3ft_map->getRowsByInstance($instanceid,$sort);
                }else{
                    $bp3ft_map= $this->m_bp3ft_map->getRows($sort);
                }
            }else{
              $bp3ft_map= $this->m_bp3ft_map->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3ft_map
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3ft_map.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'stoagetype', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $InstId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($InstId) > 0) {
            $bp3ft_map= $this->m_bp3ft_map->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3ft_map
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
        log_message('debug', 'bp3ft_map.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'stoagetype', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $ParentId  =  $this->input->get_post('parentid', TRUE);
        if (strlen($ParentId) > 0) {
            $bp3ft_map= $this->m_bp3ft_map->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3ft_map
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
        log_message('debug', 'bp3ft_map.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3ft_mapid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3ft_map->deleteRow($tempId);
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
        $this->load->model('M_bp3ft_map', 'm_bp3ft_map');
    }
}
?>