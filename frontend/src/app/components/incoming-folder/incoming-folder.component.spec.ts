import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomingFolderComponent } from './incoming-folder.component';

describe('IncomingFolderComponent', () => {
  let component: IncomingFolderComponent;
  let fixture: ComponentFixture<IncomingFolderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncomingFolderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncomingFolderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
