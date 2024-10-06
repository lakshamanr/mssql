import { Component, OnInit } from '@angular/core';
import { ProcedureService } from '../../service/procedure.service';
import { ProcedureMetaData } from '../../model/Procedure-metadata';

@Component({
  selector: 'app-procedures',
  templateUrl: './procedures.component.html',
  styleUrls: ['./procedures.component.css']
})
export class ProceduresComponent implements OnInit {

  procedureMetaData: ProcedureMetaData;

  constructor(private procedureService: ProcedureService) { }

  loadProcedureData() {
    this.procedureService.LoadStoreProcedureMetaData().subscribe(
      (data: ProcedureMetaData) => {
        // Handle the success case, e.g., save data to a variable or process it
        console.log('Procedure meta data loaded:', data);
        // You can now use the 'data' object in your component
      },
      (error) => {
        // Handle any remaining errors that weren't caught by the service
        console.error('Failed to load procedure metadata:', error);
      }
    );
  }

  ngOnInit() {
    // You can call the method when the component initializes
    this.loadProcedureData();
  }

}
