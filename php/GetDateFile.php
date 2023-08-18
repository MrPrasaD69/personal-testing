if(date('Y-m-d') >= date('Y-m-01') && date('Y-m-d') < date('Y-m-16'))
   {
      if(date('m')=='01')
      {
         $current_date = date("Y-m-01");
         $prev_year = date('Y')-1;
         $prev_date = date("$prev_year-12-16"); 
      }
      else
      {
         $current_date = date("Y-m-01");
         $prev_month = date('m', strtotime('-1 month')); 
         $prev_date = date("Y-$prev_month-16");
      }

      if(strpos($prev_date,"-01")!==false)
      {
         $prev_date_arr = explode("-",$prev_date);
         $prev_date_start = $prev_date;
         $prev_date_end = $prev_date_arr[0]."-".$prev_date_arr[1]."-15";
      }
      else
      {
         $prev_date_arr = explode("-",$prev_date);
         $prev_date_start = $prev_date;
         $prev_date_end = date($prev_date_arr[0]."-".$prev_date_arr[1]."-t");
      }

      if(strpos($current_date,"-01")!==false)
      {
         $current_date_arr = explode("-",$current_date);
         $current_date_start = $current_date;
         $current_date_end = $current_date_arr[0]."-".$current_date_arr[1]."-15";
      }
      else
      {
         $current_date_arr = explode("-",$current_date);
         $current_date_start = $current_date;
         $current_date_end = date($current_date_arr[0]."-".$current_date_arr[1]."-t");
      }
   }
   else
   {
      $current_date = date("Y-m-16");
      $prev_date = date('Y-m-01');

      if(strpos($prev_date,"-01")!==false)
      {
         $prev_date_arr = explode("-",$prev_date);
         $prev_date_start = $prev_date;
         $prev_date_end = $prev_date_arr[0]."-".$prev_date_arr[1]."-15";
      }
      else
      {
         $prev_date_arr = explode("-",$prev_date);
         $prev_date_start = $prev_date;
         $prev_date_end = date($prev_date_arr[0]."-".$prev_date_arr[1]."-t");
      }

      if(strpos($current_date,"-01")!==false)
      {
         $current_date_arr = explode("-",$current_date);
         $current_date_start = $current_date;
         $current_date_end = $current_date_arr[0]."-".$current_date_arr[1]."-15";
      }
      else
      {
         $current_date_arr = explode("-",$current_date);
         $current_date_start = $current_date;
         $current_date_end = date($current_date_arr[0]."-".$current_date_arr[1]."-t");
      }
   }