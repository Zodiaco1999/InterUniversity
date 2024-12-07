import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InscripcionProgramaComponent } from './inscripcion-programa.component';

describe('InscripcionProgramaComponent', () => {
  let component: InscripcionProgramaComponent;
  let fixture: ComponentFixture<InscripcionProgramaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InscripcionProgramaComponent]
    });
    fixture = TestBed.createComponent(InscripcionProgramaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
