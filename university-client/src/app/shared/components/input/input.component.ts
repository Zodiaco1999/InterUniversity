import { Component, Input } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss']
})
export class InputComponent {
  @Input() id = '';
  @Input() placeholder = '';
  @Input() type = 'text';
  @Input() formBuilder: FormBuilder | any;
  @Input() readOnly = false;
}
