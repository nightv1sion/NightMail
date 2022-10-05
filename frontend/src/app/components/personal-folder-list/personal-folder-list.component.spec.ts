import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalFolderListComponent } from './personal-folder-list.component';

describe('PersonalFolderListComponent', () => {
  let component: PersonalFolderListComponent;
  let fixture: ComponentFixture<PersonalFolderListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PersonalFolderListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PersonalFolderListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
