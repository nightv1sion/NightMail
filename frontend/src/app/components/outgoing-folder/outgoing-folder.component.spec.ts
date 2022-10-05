import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OutgoingFolderComponent } from './outgoing-folder.component';

describe('OutgoingFolderComponent', () => {
  let component: OutgoingFolderComponent;
  let fixture: ComponentFixture<OutgoingFolderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OutgoingFolderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OutgoingFolderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
