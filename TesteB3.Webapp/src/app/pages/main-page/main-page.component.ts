import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { merge } from 'rxjs';
import {MatCardModule} from '@angular/material/card';
import { TesteB3Api } from './../../apis/testeb3.api';
import { CdbViewModel } from '../../models/cdb.view.model';
import { CdbResponseModel } from '../../models/cdb.response.model';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [
    CommonModule, FormsModule, ReactiveFormsModule,
    MatFormFieldModule, MatInputModule, MatButtonModule,
    MatCardModule],
  changeDetection: ChangeDetectionStrategy.Default,
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})
export class MainPageComponent {
  readonly value = new FormControl<number | null>(null, [Validators.required, Validators.min(0.01)]);
  valueErrorMessage = signal('');
  
  readonly interval = new FormControl<number | null>(null, [Validators.required, Validators.min(2)]);
  intervalErrorMessage = signal('');

  resultModel: CdbResponseModel = new CdbResponseModel();
  errors: Array<string> = [];

  constructor(private testeB3Api: TesteB3Api) {
    merge(this.value.statusChanges, this.value.valueChanges)
      .pipe(takeUntilDestroyed())
      .subscribe(() => this.updateValueErrorMessage());

    merge(this.interval.statusChanges, this.interval.valueChanges)
      .pipe(takeUntilDestroyed())
      .subscribe(() => this.updateIntervalErrorMessage());
  }
  
  updateValueErrorMessage() {
    if (this.value.hasError('required')) {
      this.valueErrorMessage.set('Você deve preencher um valor');
    } else if (this.value.hasError('min')) {
      this.valueErrorMessage.set('O valor deve ser maior que R$ 0,00');
    } else {
      this.valueErrorMessage.set('');
    }
  }
  
  updateIntervalErrorMessage() {
    if (this.interval.hasError('required')) {
      this.intervalErrorMessage.set('Você deve preencher um intervalo');
    } else if (this.interval.hasError('min')) {
      this.intervalErrorMessage.set('O valor deve ser maior que 1');
    } else {
      this.intervalErrorMessage.set('');
    }
  }

  calculate() {
    let model: CdbViewModel = new CdbViewModel(this.value.value as number, this.interval.value as number);
    this.testeB3Api.getAllHousingLocations(model)
      .subscribe({
        next: (result) => this.resultModel = result,
        error: (error) => this.errors = error.messages
      })
  }

  reset() {
    this.value.reset();
    this.interval.reset();
    this.resultModel = new CdbResponseModel();
  }
}