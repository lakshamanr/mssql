import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProceduresComponent } from './components/procedures/procedures.component';
import { ProcedureComponent } from './components/procedure/procedure.component';
import { ProcedureService } from './service/procedure.service';



@NgModule({
  declarations: [ProceduresComponent, ProcedureComponent],
  imports: [
    CommonModule
  ],
  providers: [ProcedureService]
})
export class ProcedureModule { }
