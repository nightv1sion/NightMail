import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisabledBorderedButtonComponent } from './disabled-bordered-button.component';

describe('DisabledBorderedButtonComponent', () => {
  let component: DisabledBorderedButtonComponent;
  let fixture: ComponentFixture<DisabledBorderedButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DisabledBorderedButtonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DisabledBorderedButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
