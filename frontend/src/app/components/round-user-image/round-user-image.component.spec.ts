import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoundUserImageComponent } from './round-user-image.component';

describe('RoundUserImageComponent', () => {
  let component: RoundUserImageComponent;
  let fixture: ComponentFixture<RoundUserImageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoundUserImageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoundUserImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
