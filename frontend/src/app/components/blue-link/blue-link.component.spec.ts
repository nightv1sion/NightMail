import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlueLinkComponent } from './blue-link.component';

describe('BlueLinkComponent', () => {
  let component: BlueLinkComponent;
  let fixture: ComponentFixture<BlueLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BlueLinkComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BlueLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
