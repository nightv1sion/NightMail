import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogCreateMailComponent } from './dialog-create-mail.component';

describe('DialogCreateMailComponent', () => {
  let component: DialogCreateMailComponent;
  let fixture: ComponentFixture<DialogCreateMailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogCreateMailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DialogCreateMailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
