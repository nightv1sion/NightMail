import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoundUserInitialsComponent } from './round-user-initials.component';

describe('RoundUserInitialsComponent', () => {
  let component: RoundUserInitialsComponent;
  let fixture: ComponentFixture<RoundUserInitialsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoundUserInitialsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoundUserInitialsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
