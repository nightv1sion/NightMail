import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FolderEditionModalComponent } from './folder-edition-modal.component';

describe('FolderEditionModalComponent', () => {
  let component: FolderEditionModalComponent;
  let fixture: ComponentFixture<FolderEditionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FolderEditionModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FolderEditionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
