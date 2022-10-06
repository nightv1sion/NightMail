import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FolderCreationModalComponent } from './folder-creation-modal.component';

describe('FolderCreationModalComponent', () => {
  let component: FolderCreationModalComponent;
  let fixture: ComponentFixture<FolderCreationModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FolderCreationModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FolderCreationModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
