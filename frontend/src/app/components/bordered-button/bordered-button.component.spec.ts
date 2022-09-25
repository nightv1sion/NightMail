import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BorderedButtonComponent } from './bordered-button.component';

describe('BorderedButtonComponent', () => {
  let component: BorderedButtonComponent;
  let fixture: ComponentFixture<BorderedButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BorderedButtonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BorderedButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
