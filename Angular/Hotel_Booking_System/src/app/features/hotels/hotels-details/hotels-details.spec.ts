import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HotelsDetails } from './hotels-details';

describe('HotelsDetails', () => {
  let component: HotelsDetails;
  let fixture: ComponentFixture<HotelsDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HotelsDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HotelsDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
