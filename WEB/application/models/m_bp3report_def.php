
<?php
class  M_bp3report_def extends CI_Model {
    function getRow($empId) {
    $result = array('success' => false, 'msg' => 'No Row ID for retrive data');
	if (!empty($empId)){
	    $res = $this->jservice->get(array('Action' => 'GetRowData','FieldList'=>'B2G(bp3report_defid) as bp3report_defid, B2G(bp3report_defid) as id,B2G(instanceid) as instanceid, bp3report_def_BRIEF_F(bp3report_defid , NULL) as  brief,B2G(ReportView) reportview, bp3qry_def_BRIEF_F(reportview, NULL) as reportview_grid,caption,thecomment,reporttype, case reporttype  when 0 then \'Таблица\' when 1 then \'Двумерная матрица\' when 2 then \'Только расчет\' when 3 then \'Экспорт по WORD шаблону\' when 4 then \'Экспорт по Excel шаблону\' else \'\'  end   as reporttype_grid,name,reportfile,reportfile_ext', 'PartName' => 'bp3report_def', 'ID' =>  $empId 	));
	    if (!empty($res)) {
	        $result = $res[0];
	    }
	}
	return $result;
    }
    function setRow($data)  {
	    $data = (array)$data;
//////////////////////////// support file data for: reportfile
    if ($_FILES['reportfile_fu']['size'] > 0 ) {
       $ext = pathinfo($_FILES['reportfile_fu']['name'], PATHINFO_EXTENSION);
       $orig = pathinfo($_FILES['reportfile_fu']['name'], PATHINFO_BASENAME) ;
       $filename = strtolower(trim($this->jservice->get(array('Action' => 'NewGuid')),'{}') . '.' . $ext);
       if (move_uploaded_file($_FILES['reportfile_fu']['tmp_name'], $this->jservice->temp_file_path().$filename)) {
          $file_data = file_get_contents($this->jservice->temp_file_path().$filename);
          $this->jservice->get(array('Action' => 'AddFile', 'FileName'=>$filename, 'Data'=>utf8_encode($file_data), 'FileID'=>'iu_files',
            'OrigName'=>$orig   ));
          unlink($this->jservice->temp_file_path().$filename);
       }
	   if (!empty($data)) {
	        $data['reportfile']=$filename;
	        $data['reportfile_ext']=$ext;
       }
    }
//////////////////////////// end support file data for: reportfile
	if (!empty($data)) {
	    if (empty($data['bp3report_defid'])) {
	        $data['bp3report_defid'] = $this->jservice->get(array('Action' => 'NewGuid'));
	        $res=$this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3report_def', 'RowID' => $data['bp3report_defid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }else{
	    $res = $this->jservice->get(array('Action' => 'UpdateRow', 'PartName' => 'bp3report_def', 'RowID' => $data['bp3report_defid'], 'DocumentID' => $data['instanceid'], 'Values'=>$data));
	       if(isset($res[0])){
	       	if($res[0]->result!='ok'){
	       		return array('success' => FALSE, 'msg' => $res[0]->result);
	       	} 
	       }else{
	       	return array('success' => FALSE, 'msg' => 'Unknown error' );
	       }
	    }
	    return array('success' => TRUE, 'data' => $this->getRow($data['bp3report_defid'] ));
	} else {
	    return array('success' => FALSE, 'msg' => 'No data to store on server');
	}
    }
    function newRow($instanceid,$data)  {
	  $id = $this->jservice->get(array('Action' => 'NewGuid'));
	if ($this->jservice->get(array('Action' => 'NewRow', 'PartName' => 'bp3report_def', 'RowID' => $id, 'DocumentID' => $instanceid, 'Values'=>$data)) == 'OK') {
	    return array('success' => TRUE, 'data' => $id);
	} else {
	    return array('success' => FALSE, 'msg' => 'Error while create new id');
	}
    }
    function getRows($sort=array())
		{
	    $res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3report_defid) as bp3report_defid, B2G(bp3report_defid) as id,B2G(instanceid) as instanceid, bp3report_def_BRIEF_F(bp3report_defid , NULL) as  brief,B2G(ReportView) reportview, bp3qry_def_BRIEF_F(reportview, NULL) as reportview_grid,caption,thecomment,reporttype, case reporttype  when 0 then \'Таблица\' when 1 then \'Двумерная матрица\' when 2 then \'Только расчет\' when 3 then \'Экспорт по WORD шаблону\' when 4 then \'Экспорт по Excel шаблону\' else \'\'  end   as reporttype_grid,name,reportfile,reportfile_ext', 'ViewName' => 'bp3report_def'));
	    if (count($res)) {
	        return $res;
	    } else {
	        return null;
	    }
		}
    function getRowsByInstance($id,$sort=array())
		{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3report_defid) as bp3report_defid, B2G(bp3report_defid) as id,B2G(instanceid) as instanceid, bp3report_def_BRIEF_F(bp3report_defid , NULL) as  brief,B2G(ReportView) reportview, bp3qry_def_BRIEF_F(reportview, NULL) as reportview_grid,caption,thecomment,reporttype, case reporttype  when 0 then \'Таблица\' when 1 then \'Двумерная матрица\' when 2 then \'Только расчет\' when 3 then \'Экспорт по WORD шаблону\' when 4 then \'Экспорт по Excel шаблону\' else \'\'  end   as reporttype_grid,name,reportfile,reportfile_ext', 'ViewName' => 'bp3report_def', 'WhereClause' => 'instanceid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
		}
    function getRowsByParent($id,$sort=array())
	{
	$res = $this->jservice->get(array('Action' => 'GetViewData','Sort'=>$sort,'FieldList'=>'B2G(bp3report_defid) as bp3report_defid, B2G(bp3report_defid) as id,B2G(instanceid) as instanceid, bp3report_def_BRIEF_F(bp3report_defid , NULL) as  brief,B2G(ReportView) reportview, bp3qry_def_BRIEF_F(reportview, NULL) as reportview_grid,caption,thecomment,reporttype, case reporttype  when 0 then \'Таблица\' when 1 then \'Двумерная матрица\' when 2 then \'Только расчет\' when 3 then \'Экспорт по WORD шаблону\' when 4 then \'Экспорт по Excel шаблону\' else \'\'  end   as reporttype_grid,name,reportfile,reportfile_ext', 'ViewName' => 'bp3report_def', 'WhereClause' => ' parentstructrowid=G2B(\''. $id . '\')'));
	if (count($res) == 0) {
	    return null;
	} else {
	    return $res;
	}
  }
    function deleteRow($id = null) {
	  if (!empty($id) && $this->jservice->get(array('Action' => 'DeleteRow', 'PartName' => 'bp3report_def', 'RowID' => $id)) == 'OK')
	    $result = array('success' => TRUE);
	else
	    $result = array('success' => FALSE, 'msg' => 'Error while deleting record!');
	return $result;
    }
}
?>