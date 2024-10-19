import { ChangeDetectionStrategy, Component } from '@angular/core';
import { TesteB3Api } from './../../apis/testeb3.api';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})
export class MainPageComponent {
  constructor(private testeB3Api: TesteB3Api) {

  }

  calculate() {

  }
}
