import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToatsComponent } from './toats.component';

describe('ToatsComponent', () => {
  let component: ToatsComponent;
  let fixture: ComponentFixture<ToatsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ToatsComponent]
    });
    fixture = TestBed.createComponent(ToatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
