import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TryingOut } from './trying-out';

describe('TryingOut', () => {
  let component: TryingOut;
  let fixture: ComponentFixture<TryingOut>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TryingOut]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TryingOut);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
